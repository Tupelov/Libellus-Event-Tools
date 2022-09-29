using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using LibellusLibrary.Utils;
using LibellusLibrary.Utils.IO;

namespace LibellusLibrary.Event.Types
{
	internal class PmdData_Unit : PmdDataType, ITypeCreator, IExternalFile, IReferenceType
	{
		[JsonInclude]
		public List<Pmd_UnitDef> Units {get; set;}

		public PmdDataType? CreateType(uint version)
		{
			return new PmdData_Unit();
		}

		public async Task LoadExternalFile(string directory)
		{
			foreach(var unit in Units)
			{
				unit.UnitData = await File.ReadAllBytesAsync(directory + "\\" + unit.FileName);
			}
			
		}

		public async Task SaveExternalFile(string directory)
		{
			foreach (var unit in Units)
			{
				await File.WriteAllBytesAsync(Path.Combine(directory, unit.FileName), unit.UnitData);
			}
		}

		public PmdDataType? ReadType(BinaryReader reader, uint version, List<PmdTypeID> typeIDs, PmdTypeFactory typeFactory)
		{
			if (Units == null)
				Units = new();
			var OriginalPos = reader.BaseStream.Position;

			reader.BaseStream.Position = OriginalPos + 0x4;
			var size = reader.ReadUInt32();

			reader.BaseStream.Position = OriginalPos + 0x8;
			var count = reader.ReadUInt32();

			reader.BaseStream.Position = OriginalPos + 0xC;
			reader.BaseStream.Position = (long)reader.ReadUInt32();
			var unitStart = reader.FTell();
			for(int i = 0; i < count; i++)
			{
				reader.FSeek(unitStart + i * 0x20);
				var unit = new Pmd_UnitDef();
				unit.ReadUnit(reader, typeFactory);
				Units.Add(unit);
			}

			reader.BaseStream.Position = (long)OriginalPos;
			return this;

		}

		internal override void SaveData(PmdBuilder builder,BinaryWriter writer)
		{
			var start = writer.FTell();
			var dataOffset = writer.FTell()+0x20*Units.Count;

			for(int i=0; i < Units.Count;i++)
			{
				writer.FSeek(start + i * 0x20);
				Units[i].WriteUnit(writer, dataOffset);
				dataOffset += Units[i].UnitData.Length;
			}

		}

		public void SetReferences(PmdBuilder pmdBuilder)
		{
			foreach(Pmd_UnitDef unit in Units)
			{
				unit.SetReferences(pmdBuilder);
			}
		}

		internal override int GetCount() => Units.Count;
	}
	internal class Pmd_UnitDef: IReferenceType
	{
		public string FileName { get; set; }
		public byte[] UnitData;

		public int MajorID { get; set; }
		public int MinorID { get; set; }
		[JsonConverter(typeof(JsonStringEnumConverter))]
		public ObjectType ModelType { get; set; }

		public int NameIndex;

		public void ReadUnit(BinaryReader reader, PmdTypeFactory typeFactory)
		{
			FileName = typeFactory.GetNameTable(reader)[reader.ReadInt32()];
			reader.ReadInt32(); // FileIndex, should be the same as above
			MajorID = reader.ReadInt32();
			MinorID = reader.ReadInt32();
			var offset = reader.ReadInt32();
			var size = reader.ReadInt32();
			ModelType = (ObjectType)reader.ReadInt32();
			reader.ReadInt32();// reserve

			var originalpos = reader.BaseStream.Position;
			reader.FSeek(offset);
			UnitData = reader.ReadBytes(size);
			reader.BaseStream.Position = originalpos;
		}
		public void WriteUnit(BinaryWriter writer,long unitDataOffset)
		{
			writer.Write(NameIndex);
			writer.Write(NameIndex);

			writer.Write(MajorID);
			writer.Write(MinorID);
			writer.Write((int)unitDataOffset);
			writer.Write(((int)UnitData.Length));
			writer.Write((int)ModelType);
			writer.Write((int)0); // Reserve

			writer.FSeek(unitDataOffset);
			writer.Write(UnitData);
		}

		public void SetReferences(PmdBuilder pmdBuilder)
		{
			byte[] temp = Text.StringtoASCII8(FileName);
			System.Array.Resize(ref temp, 32);
			NameIndex = pmdBuilder.AddReference(PmdTypeID.Name, temp);
		}

		public enum ObjectType
		{
			Attachment = 1,
			Field_Object = 2
		}
	}
}
