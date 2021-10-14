// Copied(with permission) from TGE's EvtTool
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LibellusLibrary.Converters
{
    public sealed class ByteArrayJsonConverter : JsonConverter<byte[]>
    {
        public override void WriteJson( JsonWriter writer, byte[] value, JsonSerializer serializer )
        {
            if ( value == null )
            {
                writer.WriteNull();
                return;
            }

            byte[] data = ( byte[] )value;

            writer.Formatting = Formatting.None;

            // Compose an array.
            writer.WriteRaw( " " );
            writer.WriteStartArray();

            for ( var i = 0; i < data.Length; i++ )
            {
                writer.WriteRaw( " " );
                writer.WriteRaw( data[i].ToString() );
                if ( i != data.Length - 1 )
                    writer.WriteRaw( "," );
            }

            writer.WriteRaw( " " );
            writer.WriteEndArray();

            writer.Formatting = Formatting.Indented;
        }

        public override byte[] ReadJson( JsonReader reader, Type objectType, byte[] existingValue, bool hasExistingValue, JsonSerializer serializer )
        {
            if ( reader.TokenType == JsonToken.StartArray )
            {
                var byteList = new List<byte>();

                while ( reader.Read() )
                {
                    switch ( reader.TokenType )
                    {
                        case JsonToken.Integer:
                            byteList.Add( Convert.ToByte( reader.Value ) );
                            break;
                        case JsonToken.EndArray:
                            return byteList.ToArray();
                        case JsonToken.Comment:
                            // skip
                            break;
                        default:
                            throw new JsonSerializationException(
                                $"Unexpected token when reading bytes: {reader.TokenType}" );
                    }
                }

                throw new JsonSerializationException( "Unexpected end when reading bytes." );
            }
            else
            {
                throw new JsonSerializationException(
                    "Unexpected token parsing binary. " + $"Expected StartArray, got {reader.TokenType}." );
            }
        }
    }

}