using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

namespace Common
{
    public class AmplifierSystem
    {
        protected struct AmplifierValues
        {
            public double baseValue;
            public double additiveValue;
            public double additiveMultiplier;
            public double trueMuliplier;

            public AmplifierValues(double baseValue)
            {
                this.baseValue = baseValue;
                additiveValue = 0;
                additiveMultiplier = 1;
                trueMuliplier = 1;
            }

            public void Reset()
            {
                additiveValue = 0;
                additiveMultiplier = 1;
                trueMuliplier = 1;
            }
            public void RegisterAmplifier(Amplifier amplifier)
            {
                switch (amplifier.amplifierType)
                {
                    case AmplifierType.Plus:
                        additiveValue += amplifier.value;
                        break;
                    case AmplifierType.AdditivePercentage:
                        additiveMultiplier += amplifier.value / 100;
                        break;
                    case AmplifierType.TruePercentage:
                        trueMuliplier *= 1 + amplifier.value / 100;
                        break;
                    default:
                        break;
                }
            }
            public readonly double GetBuffedValue()
            {
                return (baseValue + additiveValue) * additiveMultiplier * trueMuliplier;
            }

            public AmplifierValues Add(AmplifierValues other)
            {
                AmplifierValues addedValues = new()
                {
                    baseValue = baseValue + other.baseValue,
                    additiveValue = additiveValue + other.additiveValue,
                    additiveMultiplier = additiveMultiplier + (other.additiveMultiplier - 1) / 100,
                    trueMuliplier = trueMuliplier * other.trueMuliplier
                };

                return addedValues;
            }
        }

        private readonly Dictionary<Stat, AmplifierValues> statDatas = new();
        private readonly Dictionary<string, Amplifier> amplifiers = new();
        private readonly Dictionary<Amplifier, int> amplifierStacking = new();

        private List<AmplifierSystem> otherAmplifierSystems = new();

        public AmplifierSystem()
        {
            
        }
        public AmplifierSystem(IEnumerable<Stat> stats, IEnumerable<double> values)
        {
            for (int i = 0; i < stats.Count(); i++)
            {
                statDatas.TryAdd(stats.ElementAt(i), new(values.ElementAt(i)));
            }
        }

        public AmplifierSystem(ICollection<Stat> stats)
        {
            foreach (var stat in stats)
            {
                statDatas.TryAdd(stat, new(stat.defaultValue));
            }
        }

        public bool RegisterStat(Stat stat, double value)
        {
            return statDatas.TryAdd(stat, new(value));
        }
        public void RegisterStats(ICollection<Stat> stats, ICollection<double> values)
        {
            for (int i = 0; i < stats.Count(); i++)
            {
                RegisterStat(stats.ElementAt(i), values.ElementAt(i));
            }
        }

        public void RegisterStats(ICollection<Stat> stats)
        {
            RegisterStats(stats, stats.Select(x => x.defaultValue).ToList());
        }

        public void RegisterAmplifiers(Amplifier amplifier)
        {
            if (amplifier == null)
            {
                return;
            }
            if (!statDatas.ContainsKey(amplifier.stat))
            {
                RegisterStat(amplifier.stat,amplifier.stat.defaultValue);
                return;
            }
            if (amplifiers.TryAdd(amplifier.uniqueTag, amplifier))
            {
                amplifierStacking.TryAdd(amplifier, 0);
            }
            int currentStacking = amplifierStacking[amplifier];
            if (currentStacking < amplifier.stackCount)
            {
                amplifierStacking[amplifier] = amplifierStacking[amplifier] + 1;
            }
            CalculateAmplifierValues(amplifier.stat);
        }
        public void RegisterAmplifiers(ICollection<Amplifier> amplifiers)
        {
            if (amplifiers == null)
            {
                return;
            }
            foreach (var item in amplifiers)
            {
                RegisterAmplifiers(item);
            }
        }

        public void UnRegisterAmplifiers(Amplifier amplifier)
        {
            if (amplifier == null)
            {
                return;
            }

            if (amplifiers.TryGetValue(amplifier.uniqueTag, out Amplifier foundAmplifier))
            {
                amplifiers.Remove(amplifier.uniqueTag);
                int stacking = amplifierStacking[foundAmplifier];
                if (stacking > 0)
                {
                    amplifierStacking[foundAmplifier] = amplifierStacking[foundAmplifier] - 1;
                }
                else
                {
                    amplifierStacking.Remove(foundAmplifier);
                }
                CalculateAmplifierValues(amplifier.stat);
            }
        }
        public void UnRegisterAmplifiers(ICollection<Amplifier> amplifiers)
        {
            if (amplifiers == null)
            {
                return;
            }
            foreach (var item in amplifiers)
            {
                UnRegisterAmplifiers(item);
            }
        }

        private void CalculateAmplifierValues(Stat stat)
        {
            statDatas[stat].Reset();
            var amplifiersInContext = amplifiers.Where(x => x.Value.stat == stat).Select(x => x.Value);
            foreach (var amplifier in amplifiersInContext)
            {
                int stacking = amplifierStacking[amplifier];
                for (int i = 0; i < stacking + 1; i++)
                {
                    statDatas[stat].RegisterAmplifier(amplifier);
                }
            }
        }

        public void SetOtherAmplifierSystems(List<AmplifierSystem> amplifierSystems)
        {
            otherAmplifierSystems = amplifierSystems;
        }

        protected Dictionary<Stat,AmplifierValues> GetStatDatas()
        {
            return statDatas;
        }

        public bool TryGetBuffedAttributeValue(Stat stat, out double value)
        {
            AmplifierValues addedValues = new(0);
            foreach (var otherSystems in otherAmplifierSystems)
            {
                var statData = otherSystems.GetStatDatas();
                if (statData.TryGetValue(stat, out AmplifierValues ampValues))
                {
                    addedValues = ampValues.Add(addedValues);
                }
            }
            if (statDatas.TryGetValue(stat, out AmplifierValues values))
            {
                addedValues = addedValues.Add(values);
                value = addedValues.GetBuffedValue();
                return true;
            }
            value = -1;
            return false;
        }
    }
}
