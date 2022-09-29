using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LibellusLibrary.JSON
{
	public static class Utilities
	{
		public static string BeautifyJson(string json)
		{
			using JsonDocument document = JsonDocument.Parse(json);
			using var stream = new MemoryStream();
			using var writer = new Utf8JsonWriter(stream, new JsonWriterOptions() { Indented = true });
			document.WriteTo(writer);
			writer.Flush();
			return Encoding.UTF8.GetString(stream.ToArray());
		}

		// Assumes that we are right after start object
		public static Dictionary<string,object> ReadJSONTokens(this Utf8JsonReader reader)
		{
			var tokens = new Dictionary<string,object>();
			if (reader.TokenType == JsonTokenType.StartObject)
				reader.Read();

			while (reader.TokenType!=JsonTokenType.EndObject)
			{
				var tokenName = reader.GetString();
				reader.Read();
				object value = null;
				 switch (reader.TokenType)
				{
					case JsonTokenType.String:
							value = reader.GetString();
							
							break;

					case JsonTokenType.Number:
							value = reader.GetInt64();
							tokens.Add(tokenName, value);
							reader.Read();
						break;
					//case JsonTokenType.
				};
				tokens.Add(tokenName, value);
				reader.Read();
			}
	
			return tokens;
		}
	}
}
