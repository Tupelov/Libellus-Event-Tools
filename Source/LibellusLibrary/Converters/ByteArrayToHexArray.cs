using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibellusLibrary.Converters
{
	class ByteArrayToHexArray: JsonConverter
	{
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(byte[]);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                var hex = serializer.Deserialize<string>(reader);
                if (!string.IsNullOrEmpty(hex))
                {
                    hex = hex.Replace(" ", string.Empty);
                    return Enumerable.Range(0, hex.Length)
                         .Where(x => x % 2 == 0)
                         .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                         .ToArray();
                }
            }
            return Enumerable.Empty<byte>();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var bytes = value as byte[];
            var @string = BitConverter.ToString(bytes).Replace("-", " ");
            serializer.Serialize(writer, @string);
        }
    }
}
