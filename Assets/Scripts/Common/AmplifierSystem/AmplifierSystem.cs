using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Common
{
    public class AmplifierSystem: IAmplifierSystemOwner
    {
        private readonly Dictionary<Stat, AmplifierSystemData> statDatas = new();
        private readonly Dictionary<string, Amplifier> amplifiers = new();
        private readonly Dictionary<Amplifier, int> amplifierStacking = new();

        private List<AmplifierSystem> otherAmplifierSystems = new();

        public AmplifierSystem()
        {
            
        }
        public AmplifierSystem(ICollection<Stat> stats, ICollection<double> values)
        {
            for (int i = 0; i < stats.Count; i++)
            {
                statDatas.TryAdd(stats.ElementAt(i), new(values.ElementAt(i)));
            }
        }

        public AmplifierSystem(ICollection<StatSelector> stats) : this(stats.Select(x=>x.stat).ToArray(),stats.Select(x=>x.value).ToArray())
        {
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
                for (int i = 0; i < stacking; i++)
                {
                    statDatas[stat].RegisterAmplifier(amplifier);
                }
            }
        }

        public void SetOtherAmplifierSystems(List<AmplifierSystem> amplifierSystems)
        {
            otherAmplifierSystems = amplifierSystems;
        }

        public void AddAmplifierSystem(AmplifierSystem amplifierSystem)
        {
            otherAmplifierSystems.Add(amplifierSystem);
        }

        public void RemoveAmplifierSystem(AmplifierSystem amplifierSystem)
        {
            otherAmplifierSystems.Remove(amplifierSystem);
        }

        public void ClearOtherAmplifierSystems()
        {
            otherAmplifierSystems = new();
        }

        public Dictionary<Stat, AmplifierSystemData> GetStatDatas()
        {
            return statDatas;
        }

        public bool TryGetBuffedAttributeValue(Stat stat, out double value)
        {
            AmplifierSystemData addedValues = new(0);
            bool hasOtherData = false;
            foreach (var otherSystems in otherAmplifierSystems)
            {
                var statData = otherSystems.GetStatDatas();
                if (statData.TryGetValue(stat, out AmplifierSystemData ampValues))
                {
                    addedValues.Add(ampValues);
                    hasOtherData = true;
                }
            }
            if (statDatas.TryGetValue(stat, out AmplifierSystemData values))
            {
                addedValues.Add(values);
                addedValues.baseValue = values.baseValue;
                value = addedValues.GetBuffedValue();
                return true;
            }
            if (hasOtherData)
            {
                addedValues.baseValue = stat.defaultValue;
                value = addedValues.GetBuffedValue();
                return true;
            }
            value = -1;
            return false;
        }

        public AmplifierSystem GetAmplifierSystem()
        {
            return this;
        }
    }
}
