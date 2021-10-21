using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibellusLibrary.Converters
{
	internal class CharArrayToStringConverter: JsonConverter
    {

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(char[]);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                var hex = serializer.Deserialize<string>(reader);
                return hex.ToCharArray();
            }
            return Enumerable.Empty<char>().ToArray();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, new string((char[])value));
        }
    }
}
