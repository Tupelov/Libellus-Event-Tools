using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using LibellusLibrary.IO;
using System.Diagnostics;
using LibellusLibrary.PmdFile.DataTypes;
using LibellusLibrary.PmdFile.Common;
using Newtonsoft.Json;

namespace LibellusLibrary.PmdFile
{

	public class PmdFile : FileBase
	{

		//Header
		[JsonIgnore] public byte FileType;
		[JsonIgnore] public byte FileFormat;
		[JsonIgnore] public short UserID;
		[JsonIgnore] public int FileSize => getFileSize();
		public char[] MagicCode;//PMD1/PMD2/PMD3
		[JsonIgnore] public int ExpandSize;
		[JsonIgnore] public int TypeTableCount => TypeTable.Count;//Expands to get{return TypeTable.Count}
		public int Version;
		[JsonIgnore] public int Reserve2;
		[JsonIgnore] public int Reserve3;

		public List<PmdTypeTable> TypeTable;

		public PmdFile() { }
		public PmdFile(string path) { Open(path); }
		public PmdFile(Stream stream, bool leaveOpen = false) { Open(stream, leaveOpen); }
		public PmdFile(BinaryReader reader) { Open(reader); }

		internal int getFileSize()
		{
			int fileSize = 0x20;//Header Size
			fileSize = fileSize + (0x10 * TypeTableCount);//TypeTableSize

			foreach (PmdTypeTable typeTableEntry in TypeTable)
			{
				fileSize += typeTableEntry.ItemSize * typeTableEntry.ItemCount;
			}
			return fileSize;
		}

		internal override void Read(BinaryReader reader)
		{
			FileType = reader.ReadByte();
			FileFormat = reader.ReadByte();
			UserID = reader.ReadInt16();
			//FileSize = reader.ReadInt32();
			reader.FSkip(4);//FileSize is set using a setter
			MagicCode = reader.ReadChars(4);
			//Console.WriteLine(MagicCode);
			ExpandSize = reader.ReadInt32();
			int typeTablecnt = reader.ReadInt32();
			Version = reader.ReadInt32();

			//Console.WriteLine("PMD File Ver.{0}", Version);

			Reserve2 = reader.ReadInt32();
			Reserve3 = reader.ReadInt32();
			TypeTable = new List<PmdTypeTable>();

			for (int i = 0; i < typeTablecnt; i++)
			{
				TypeTable.Add(new PmdTypeTable(reader));
			}

			return;

		}

		internal override void Write(BinaryWriter writer)
		{
			writer.Write(FileType);
			writer.Write(FileFormat);
			writer.Write(UserID);
			writer.Write(FileSize);
			writer.Write(MagicCode);
			writer.Write(ExpandSize);
			writer.Write(TypeTableCount);
			writer.Write(Version);
			writer.Write(Reserve2);
			writer.Write(Reserve3);

			//First we write while adjusting the data offsets
			long dataStart = writer.FTell() + (0x10 * TypeTableCount);
			long dataCurrent = dataStart;
			foreach (PmdTypeTable typeTableEntry in TypeTable)
			{
				typeTableEntry.ItemAddress = (int)dataCurrent;
				typeTableEntry.Write(writer);
				dataCurrent += typeTableEntry.ItemSize * typeTableEntry.ItemCount;
			}
			writer.FSeek(dataStart);
			//Now we write the data
			foreach (PmdTypeTable typeTableEntry in TypeTable)
			{
				foreach (PmdDataType dataTypeEntry in typeTableEntry.DataTable)
				{
					dataTypeEntry.Write(writer);
				}
			}
		}

		public string ToJson()
		{
			return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented, new Newtonsoft.Json.JsonSerializerSettings
			{
				TypeNameHandling = TypeNameHandling.Auto

			});
		}

		public static PmdFile FromJson(string json)
		{
			return Newtonsoft.Json.JsonConvert.DeserializeObject<PmdFile>(json, new JsonSerializerSettings

			{
				TypeNameHandling = TypeNameHandling.Auto

			});
		}

	}

	[DebuggerDisplay("Type: {Type}")]
	public class PmdTypeTable : FileBase
	{

		public DataTypeID Type;
		public int ItemSize;
		[JsonIgnore] public int ItemCount => DataTable.Count;
		[JsonIgnore] public int ItemAddress;

		public List<PmdDataType> DataTable;

		public PmdTypeTable() { }
		public PmdTypeTable(string path) { Open(path); }
		public PmdTypeTable(Stream stream, bool leaveOpen = false) { Open(stream, leaveOpen); }
		public PmdTypeTable(BinaryReader reader) { Open(reader); }

		public PmdTypeTable(DataTypeID type, int itemSize, List<PmdDataType> dataTable)
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
			DataTable = new List<PmdDataType>();

			long currentPos = reader.FTell();
			reader.FSeek(ItemAddress);
			for (int i = 0; i < dataCount; i++)
			{
				PmdDataType Entry = PmdDataType.CreateDataType(Type, reader, ItemSize);
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

}
