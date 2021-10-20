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
using LibellusLibrary.Utils;

namespace LibellusLibrary.PMD.Types
{
	//[JsonConverter(typeof(TypeTableJsonConverter))]
	public class TypeTable : FileBase
	{
		[JsonIgnore] public FormatVersion Version { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		public DataTypeID Type;
		public int ItemSize;
		[JsonIgnore] public int ItemCount => DataTable.Count;
		[JsonIgnore] public int ItemAddress;

		[JsonConverter(typeof(DontDeserializeJsonConverter))]
		public List<DataType> DataTable;

		
		public TypeTable(FormatVersion version) { Version = version;   }
		public TypeTable(string path, FormatVersion version) { Version = version; Open(path); }
		public TypeTable(Stream stream, FormatVersion version, bool leaveOpen = false) { Version = version;  Open(stream, leaveOpen); }
		public TypeTable(BinaryReader reader, FormatVersion version) { Version = version; Open(reader); }

		public TypeTable(DataTypeID type, int itemSize, List<DataType> dataTable, Version version)
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
			ItemSize = reader.ReadInt32();
			int dataCount = reader.ReadInt32();
			ItemAddress = reader.ReadInt32();
			DataTable = new List<DataType>();

			long currentPos = reader.FTell();
			reader.FSeek(ItemAddress);
			for (int i = 0; i < dataCount; i++)
			{
				DataType Entry = TypeFactory.CreateDataType(Type, reader, Version, ItemSize);
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
		public void ReadJson(JsonReader reader, JsonSerializer serializer, FormatVersion version)
		{
			var jsonObject = JObject.Load(reader);
			serializer.Populate(jsonObject.CreateReader(), this);

			Type type = TypeFactory.GetDataType(Type, Version);

			var data = Reflection.CreateListFromType(type);
			if(type == typeof(Frame))
			{ // Special handling for frames
				for(int i = 0; i < jsonObject["DataTable"].Count(); i++)
				{
					data.Add(Frame.ReadJson(jsonObject["DataTable"][i].CreateReader(), serializer, version));
				}
				DataTable = data.Cast<DataType>().ToList();
				return;
			}
			try
			{
				serializer.Populate(jsonObject["DataTable"].CreateReader(), data);
			}
			catch (Newtonsoft.Json.JsonSerializationException e)
			{
				Console.WriteLine("An exception occured! We'll try to roll with it anyways.");
				Console.WriteLine(e);
			}
			if (data.GetType() == typeof(List<Unknown>))
			{
				foreach (Unknown unkData in data)
				{
					unkData.SetType(Type);
				}
			}
			DataTable = data.Cast<DataType>().ToList();

		}
		
	}


}
