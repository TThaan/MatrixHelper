using Newtonsoft.Json;
using System;

namespace MatrixHelper.JsonConverters
{
    // redundant ?
    public class GenericJsonConverter<T> : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(T);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new Exception();
            //serializer.TypeNameHandling = TypeNameHandling.All;
            return serializer.Deserialize<T>(reader);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //serializer.TypeNameHandling = TypeNameHandling.All;
            serializer.Serialize(writer, value);
        }
    }
}
