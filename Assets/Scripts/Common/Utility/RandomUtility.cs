using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common
{
    public static class RandomUtility
    {
        private static ZigguratNormal zigguratNormal;

        public static int RandomWeightedTable(ICollection<decimal> weights)
        {
            weights = weights.Select(x => x < 0 ? 0 : x).ToArray();
            var sum = weights.Sum();
            System.Random rnd = new();
            var rng = rnd.NextDecimal(0, sum);
            for (int i = 0; i < weights.Count; i++)
            {
                rng -= weights.ElementAt(i);
                if (rng <= 0)
                {
                    return i;
                }
            }
            return -1;
        }
        public static int RandomWeightedTable(ICollection<double> weights)
        {
            weights = weights.Select(x => x < 0 ? 0 : x).ToArray();
            var sum = weights.Sum();
            System.Random rnd = new();
            var rng = rnd.NextDouble(0, sum);
            for (int i = 0; i < weights.Count; i++)
            {
                rng -= weights.ElementAt(i);
                if (rng <= 0)
                {
                    return i;
                }
            }
            return -1;
        }
        public static int RandomWeightedTable(ICollection<float> weights)
        {
            return RandomWeightedTable(weights.Select(x => (double)x).ToArray());
        }
        public static int RandomWeightedTable(ICollection<int> weights)
        {
            return RandomWeightedTable(weights.Select(x => (double)x).ToArray());
        }

        public static void SetNormalPrecision(ZigguratNormal zigguratNormal)
        {
            RandomUtility.zigguratNormal = zigguratNormal;
        }
        public static void SetNormalPrecision(int r = 3, int n = 256, System.Random rng = null)
        {
            zigguratNormal = new(r, n, rng);
        }

        public static double RandomNormal()
        {
            zigguratNormal ??= new();
            return zigguratNormal.NextGaussian();
        }
        public static double RandomNormal(double mean, double deviation)
        {
            var num = RandomNormal();
            return mean + deviation * num;
        }

        public static decimal RandomNormalDecimal()
        {
            zigguratNormal ??= new();
            return zigguratNormal.NextGaussianDecimal();
        }
        public static decimal RandomNormalDecimal(decimal mean, decimal deviation)
        {
            var num = RandomNormalDecimal();
            return mean + deviation * num;
        }

        public static T RandomElement<T>(ICollection<T> elements)
        {
            System.Random rnd = new();
            return elements.ElementAt(rnd.Next(0,elements.Count));
        }
    }
}
