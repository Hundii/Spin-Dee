using System;
using System.Globalization;

namespace Common
{
    [Serializable]
    public class WeightedLoot : FairLoot, IQualityLoot
    {
        public string weight;
        public float quality;

        public override decimal GetChance()
        {
            weight = weight.Replace(',', '.');
            if (decimal.TryParse(weight, NumberStyles.Float, CultureInfo.InvariantCulture, out var result))
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
