using System;

namespace Common
{
    public enum AmplifierType
    {
        Plus, AdditivePercentage, TruePercentage
    }

    public enum AmplifierScope
    {
        Self, Other, All
    }

    [Serializable]
    public class Amplifier
    {
        public string uniqueTag;
        public Stat stat;
        public AmplifierType amplifierType;
        public AmplifierScope amplifierScope;
        public double value;
        public int stackCount;

        public string GetDisplayString(int valueRoundAmount = 2)
        {
            string symbol = amplifierType == AmplifierType.TruePercentage ? "* " : "+ ";
            string valueText = $"{value.AsRoundStr(valueRoundAmount)}";
            string percentage = $"{(amplifierType != AmplifierType.Plus ? "% " : " ")}";
            string statText = $"{stat.attributeName}";
            return symbol + valueText + percentage + statText;
        }

        public Amplifier()
        {
            
        }

        public Amplifier(Amplifier other)
        {
            uniqueTag = other.uniqueTag;
            stat = other.stat;
            amplifierType = other.amplifierType;
            amplifierScope = other.amplifierScope;
            value = other.value;
            stackCount = other.stackCount;
        }
    }
}
