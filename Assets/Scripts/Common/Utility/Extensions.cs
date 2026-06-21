using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Common
{
    public static class Extensions
    {
        public static T Inject<T>(this MonoBehaviour _)
        {
            return DIContainer.Instance.GetService<T>();
        }
        public static T InjectInterface<T>(this MonoBehaviour _)
        {
            return DIContainer.Instance.GetServiceByInterface<T>();
        }

        public static void GetFlattenedChildrenTree(this Transform transform, ref List<Transform> children)
        {
            foreach (Transform child in transform)
            {
                children.Add(child);
                GetFlattenedChildrenTree(child, ref children);
            }
        }

        #region String
        public static string AsRoundStr(this float value, int decimalPoints = 1)
        {
            return AsRoundStr((double)value,decimalPoints);
        }
        public static string AsRoundStr(this float value, int maxDecimalPoints = 2, int logBase = 10, int noDecimal = 1000000)
        {
            return AsRoundStr((double)value, maxDecimalPoints, logBase, noDecimal);
        }

        public static string AsRoundStr(this double value, int decimalPoints = 1)
        {
            return Math.Round(value, decimalPoints).ToString();
        }
        public static string AsRoundStr(this double value, int maxDecimalPoints = 2, int logBase = 10, int noDecimal = 1000000)
        {
            if (value > noDecimal)
            {
                return Math.Round(value).ToString();
            }
            if (value < 1)
            {
                return Math.Round(value, maxDecimalPoints).ToString();
            }
            int decimalPoints = (int)Math.Clamp(Math.Log(noDecimal, logBase) + 1 - Math.Log(value, logBase), 0, maxDecimalPoints);
            return Math.Round(value, decimalPoints).ToString();
        }
        public static string AsIndentedStr(this string value, float minValue = 0)
        {
            if (float.Parse(value) < minValue)
            {
                return value;
            }
            return (float.Parse(value).ToString("N0", new CultureInfo("fr-FR")));
        }

        public static string ToFirstLower(this string value)
        {
            char first = char.ToLower(value[0]);
            string str = first + value.Remove(0, 1);
            return str;
        }

        public static string ToPascalCase(this string value)
        {
            value = value.ToFirstLower();
            var parts = value.Split();
            return string.Join("", parts);
        }

        #endregion

        public static string ToStringJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        public static string ListToStringJson<T>(this List<T> objects)
        {
            StringBuilder str = new(objects.Count * 20);
            str.AppendJoin('\n', objects.Select(x => x.ToStringJson()));
            return str.ToString();
        }

        #region Random
        // https://stackoverflow.com/questions/609501/generating-a-random-decimal-in-c-sharp
        public static int NextInt32(this System.Random random)
        {
            int firstBits = random.Next(0, 1 << 4) << 28;
            int lastBits = random.Next(0, 1 << 28);
            return firstBits | lastBits;
        }

        public static decimal NextDecimalSample(this System.Random random)
        {
            decimal sample = 1m;
            while (sample >= 1)
            {
                var a = random.NextInt32();
                var b = random.NextInt32();
                var c = random.Next(542101087);
                sample = new(a, b, c, false, 28);
            }
            return sample;
        }

        public static decimal NextDecimal(this System.Random random)
        {
            return NextDecimal(random, decimal.One);
        }

        public static decimal NextDecimal(this System.Random random, decimal maxValue)
        {
            return NextDecimal(random, decimal.Zero, maxValue);
        }

        public static decimal NextDecimal(this System.Random random, decimal minValue, decimal maxValue)
        {
            var nextDecimalSample = NextDecimalSample(random);
            return minValue + (maxValue - minValue) * nextDecimalSample;
        }

        public static double NextDouble(this System.Random random, double minValue, double maxValue)
        {
            var nextDouble = random.NextDouble();
            return minValue + (maxValue - minValue) * nextDouble;
        }

        #endregion


    }
}
