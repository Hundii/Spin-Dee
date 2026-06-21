using Newtonsoft.Json;
using System;
using System.Numerics;

namespace Common
{
    public class BigIntegerJsonConverter : JsonConverter<BigInteger>
    {
        public override void WriteJson(JsonWriter writer, BigInteger value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        public override BigInteger ReadJson(JsonReader reader, Type objectType, BigInteger existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return BigInteger.Parse(reader.Value.ToString());
        }
    }

}
