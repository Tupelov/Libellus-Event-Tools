using System;
using System.Text;
using Newtonsoft.Json;

namespace LibellusLibrary.Converters
{
    public sealed class IntHexDumpJsonConverter : JsonConverter<byte[]>
    {
        public override byte[] ReadJson(JsonReader reader, Type objectType, byte[] existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var str = ((string)reader.Value).Replace(" ", "");
            var bytes = new byte[str.Length / 2];
            var byteIndex = 0;
            for (int i = 0; i < str.Length; i += 2)
            {
                bytes[byteIndex++] = byte.Parse(str[i].ToString() + str[i + 1].ToString(), System.Globalization.NumberStyles.HexNumber);
            }

            return bytes;
        }

        public override void WriteJson(JsonWriter writer, byte[] value, JsonSerializer serializer)
        {
            var builder = new StringBuilder();
            var first = true;
            for (int i = 0; i < value.Length; i++)
            {
                if (!first)
                {
                    builder.Append(" ");
                }
                else
                {
                    first = false;
                }

                if (i + 3 < value.Length)
                {
                    builder.Append($"{(value[i] << 24 | value[i + 1] << 16 | value[i + 2] << 8 | value[i + 3]):X8}");
                    i += 3;
                }
                else
                {
                    builder.Append($"{value[i]:X2}");
                }
            }

            writer.WriteValue(builder.ToString());
        }
    }
}