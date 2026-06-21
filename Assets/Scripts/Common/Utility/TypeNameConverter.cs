using Newtonsoft.Json;
using System;
using UnityEngine;

namespace Common
{
    public class TypeNameConverter : JsonConverter<Type>
    {
        public override void WriteJson(JsonWriter writer, Type value, JsonSerializer serializer)
        {
            writer.WriteValue(value?.FullName);
        }

        public override Type ReadJson(
            JsonReader reader,
            Type objectType,
            Type existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            string typeName = reader.Value?.ToString();

            return typeName != null
                ? Type.GetType(typeName)
                : null;
        }
    }
}
