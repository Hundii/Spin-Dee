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
    }
}
