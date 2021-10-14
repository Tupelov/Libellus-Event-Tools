using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LibellusLibrary.Json.Converters
{
    public sealed class DontDeserializeJsonConverter : JsonConverter
    {
        public override bool CanConvert( Type objectType )
        {
            return true;
        }

        public override bool CanWrite => false;

        public override object ReadJson( JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer )
        {
            // Skip JObject
            JObject.ReadFrom( reader );
            return null;
        }

        public override void WriteJson( JsonWriter writer, object value, JsonSerializer serializer )
        {
            throw new NotImplementedException();
        }
    }
}