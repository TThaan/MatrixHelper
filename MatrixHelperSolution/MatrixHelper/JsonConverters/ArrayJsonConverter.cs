using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace MatrixHelper.JsonConverters
{
    public class JsonConverter_Array<T> : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(T);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new Exception();
            //serializer.TypeNameHandling = TypeNameHandling.All;
            var result = serializer.Deserialize<T[]>(reader);
            
            //arr.CopyTo(result, 0);
            //var hm = arr.ToObject(typeof(List<T>));
            
            //T[] result = arr.ToObject(typeof(T[])) as T[];
            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //serializer.TypeNameHandling = TypeNameHandling.All;
            serializer.Serialize(writer, value);
        }
    }
}
