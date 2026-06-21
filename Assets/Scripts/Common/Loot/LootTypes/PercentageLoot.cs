using System;
using System.Globalization;

namespace Common
{
    [Serializable]
    public class PercentageLoot : FairLoot, IQualityLoot
    {
        public string chance;
        public float quality;

        public override decimal GetChance()
        {
            chance = chance.Replace(',', '.');
            if (decimal.TryParse(chance, NumberStyles.Float, CultureInfo.InvariantCulture, out var result))
            {
                return result;
            }
            return 0m;
        }

        public float GetQuality()
        {
            return quality;
        }
    }
}
