using UnityEngine;

namespace Common
{
    public class AmplifierSystemData
    {
        public double baseValue;
        public double additiveValue;
        public double additiveMultiplier;
        public double trueMuliplier;

        public AmplifierSystemData()
        {

        }

        public AmplifierSystemData(double baseValue)
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
        public double GetBuffedValue()
        {
            return (baseValue + additiveValue) * additiveMultiplier * trueMuliplier;
        }

        public void Add(AmplifierSystemData other)
        {
            additiveValue += other.additiveValue;
            additiveMultiplier += other.additiveMultiplier - 1;
            trueMuliplier *= other.trueMuliplier;
        }

        public string GetDisplayString()
        {
            return $"Value: {GetBuffedValue()}\t Base: {baseValue}\t Plus: {additiveValue}\t AdditiveMultiplier: {additiveMultiplier}\t TrueMultiplier: {trueMuliplier}";
        }
    }
}
