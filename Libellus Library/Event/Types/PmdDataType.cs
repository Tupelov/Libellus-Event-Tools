using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LibellusLibrary.Event.Types
{
	public class PmdDataType
	{
		[JsonPropertyOrder(-1)]
		[JsonConverter(typeof(JsonStringEnumConverter))]
		public PmdTypeID Type { get; set; }

		internal virtual void SaveData(PmdBuilder builder, BinaryWriter writer)=> throw new NotImplementedException(this.Type.ToString());
		internal virtual int GetCount() => throw new NotImplementedException(this.Type.ToString());
		internal virtual int GetSize() => 0; // Data size doesnt matter for certain types
	}
	
	public class PmdDataTypeConverter : JsonConverter<PmdDataType>
	{
		public override PmdDataType? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			// obj = base.Read(ref reader, typeToConvert, options);
			//var conv = new ;
			//var type = reader.TokenType;
			//reader.Read();
			//type = reader.TokenType;
			/*
			reader.Read();
			reader.Read();
			var typeID = (PmdTypeID)reader.GetInt32();
			var creator = PmdTypeFactory.GetTypeCreator(typeID);
			read
			var dataType = creator.CreateType()

			var conv = new JsonStringEnumConverter().CreateConverter(typeof(PmdTypeID), options);
			
			JsonSerializer.Deserialize<PmdTypeID>(ref reader, options);
			*/

			//var str = reader.GetString();
			//var converters = options.
			//var pmdData = new PmdDataType();
			//var obj = JsonSerializer.Deserialize(ref reader, typeof(PmdDataType),options, pmdData);
			//type = reader.TokenType;
			//var typeID = reader.GetInt32();

			var deser = JsonSerializer.Deserialize<PmdDataType>(ref reader, options);
			throw new NotImplementedException();
		}

		public override void Write(Utf8JsonWriter writer, PmdDataType value, JsonSerializerOptions options)
		{
			writer.WriteRawValue(JsonSerializer.Serialize<object>(value,options));
		}
	}

	public enum PmdTypeID
	{
		CutInfo = 0,
		Name = 1,
		Stage = 2,
		Unit = 3,
		FrameTable = 4,
		Camera = 5,
		Message = 6,
		Effect = 7,
		EffectData = 8,
		UnitData = 9,
		F1 = 10,
		F2 = 11,
		FTB = 12,
		SLight = 13,
		SFog = 14,
		Blur2 = 15,
		MultBlur = 16,
		DistBlur = 17,
		Filter = 18,
		MultFilter = 19,
		RipBlur = 20,

		ObjectTable = 21,

		RainData = 25,
		BezierTable = 26,
		RefTable = 27,
		MAX = 28
	}

}
