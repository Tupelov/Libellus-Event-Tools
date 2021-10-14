using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using LibellusLibrary.IO;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using LibellusLibrary.Json.Converters;
using System.Collections;

namespace LibellusLibrary.PMD.Types
{
	[JsonConverter(typeof(TypeTableJsonConverter))]
	public class TypeTable: FileBase
	{

		[JsonConverter(typeof(StringEnumConverter))]
		public DataTypeID Type;
		public int ItemSize;
		[JsonIgnore] public int ItemCount => DataTable.Count;
		[JsonIgnore] public int ItemAddress;

		[JsonConverter(typeof(DontDeserializeJsonConverter))]
		public List<DataType> DataTable;

		public TypeTable() { }
		public TypeTable(string path) { Open(path); }
		public TypeTable(Stream stream, bool leaveOpen = false) { Open(stream, leaveOpen); }
		public TypeTable(BinaryReader reader) { Open(reader); }

		public TypeTable(DataTypeID type, int itemSize, List<DataType> dataTable)
		{
			Type = type;
			ItemSize = itemSize;
			ItemAddress = 0;
			DataTable = dataTable;
			return;
		}


		internal override void Read(BinaryReader reader)
		{
			Type = (DataTypeID)reader.ReadInt32();
			ItemSize =reader.ReadInt32();
			int dataCount = reader.ReadInt32();
			ItemAddress = reader.ReadInt32();
			DataTable = new List<DataType>();

			long currentPos = reader.FTell();
			reader.FSeek(ItemAddress);
			for (int i = 0; i < dataCount; i++)
			{
				DataType Entry = TypeFactory.CreateDataType(Type, reader, ItemSize);
				DataTable.Add(Entry);
			}
			reader.FSeek(currentPos);
		}

		internal override void Write(BinaryWriter writer)
		{
			writer.Write((int)Type);
			writer.Write(ItemSize);
			writer.Write(ItemCount);
			writer.Write(ItemAddress);
			return;
		}

	}

	public sealed class TypeTableJsonConverter : JsonConverter<TypeTable>
	{
		static IList CreateGenericList(Type typeInList)
		{
			var genericListType = typeof(List<>).MakeGenericType(new[] { typeInList });
			return (IList)Activator.CreateInstance(genericListType);
		}

		public override void WriteJson(JsonWriter writer, TypeTable value, JsonSerializer serializer){		}
		public override bool CanWrite => false;
		public override TypeTable ReadJson(JsonReader reader, Type objectType, TypeTable existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			var jsonObject = JObject.Load(reader);
			var typeTable = new TypeTable();
			serializer.Populate(jsonObject.CreateReader(), typeTable);

			Type type = TypeFactory.GetDataType(typeTable.Type);

			var data = CreateGenericList(type);

			serializer.Populate(jsonObject["DataTable"].CreateReader(), data);
			typeTable.DataTable = data.Cast<DataType>().ToList();
				

			return typeTable;
		}
	}
}
