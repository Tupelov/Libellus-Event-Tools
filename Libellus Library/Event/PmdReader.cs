using LibellusLibrary.Event.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using LibellusLibrary.JSON;


namespace LibellusLibrary.Event
{
	public class PmdReader
	{

		public async Task<PolyMovieData> ReadPmd(BinaryReader reader)
		{
			PolyMovieData _data = new();

			uint typeTblCnt = _data.ReadHeader(reader);

			reader.BaseStream.Position = 0x20;
			PmdTypeFactory factory = new();
			_data.PmdDataTypes = factory.ReadDataTypes(reader, typeTblCnt, _data.Version);
			return _data;
		}

		public async Task<PolyMovieData> ReadPmd(Stream stream, bool leaveOpen = false)
		{
			using (BinaryReader reader = new BinaryReader(stream, Encoding.Default, leaveOpen))
			{
				return await ReadPmd(reader);
			}
		}
		public async Task<PolyMovieData> ReadPmd(string path)
		{
			if (!File.Exists(path))
			{
				throw new ArgumentException("Error while opening file.\nFile does not exist!\nFile: " + path);
			}
			using (MemoryStream stream = new MemoryStream(await File.ReadAllBytesAsync(path)))
			{
				return await ReadPmd(stream, false);
			}
		}


	}

	public class PmdJsonReader : JsonConverter<PolyMovieData>
	{
		public override PolyMovieData? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{

			var pmd = new PolyMovieData();
			//	var data = reader.ReadJSONTokens();

			reader.Read(); // startobject
			reader.Read(); // MagicCode:
			pmd.MagicCode = reader.GetString();
			reader.Read(); // ""
			reader.Read(); // Version
			pmd.Version = reader.GetUInt32();
			reader.Read(); //
			reader.Read(); // Data Table

			pmd.PmdDataTypes = new();

			reader.Read();
			List<PmdDataType> abstractTypes = new();

			var abstractReader = reader;

			while (abstractReader.TokenType != JsonTokenType.EndArray)
			{
				var abstractType = JsonSerializer.Deserialize<PmdDataType>(ref abstractReader, options);
				abstractTypes.Add(abstractType);
				abstractReader.Read();

			}

			foreach (PmdDataType abstractType in abstractTypes)
			{
				Type trueDataType = PmdTypeFactory.GetTypeCreator(abstractType.Type).CreateType(pmd.Version).GetType();



				pmd.PmdDataTypes.Add((PmdDataType)JsonSerializer.Deserialize(ref reader, trueDataType, options));
				reader.Read();
			}
			reader.Read();

			return pmd;
		}

		public override void Write(Utf8JsonWriter writer, PolyMovieData value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("Magic Code");
			writer.WriteStringValue(value.MagicCode);
			writer.WritePropertyName("Version");
			writer.WriteNumberValue(value.Version);

			writer.WritePropertyName("Data Table");
			writer.WriteStartArray();
			foreach (PmdDataType data in value.PmdDataTypes)
			{
				writer.WriteRawValue(JsonSerializer.Serialize<object>(data, options));
			}
			writer.WriteEndArray();
			writer.WriteEndObject();
		}
	}
}
