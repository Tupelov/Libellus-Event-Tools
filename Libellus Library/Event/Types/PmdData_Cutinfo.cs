using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibellusLibrary.Event.Types
{
	internal class PmdData_Cutinfo : PmdDataType, ITypeCreator, IVersionable
	{
		
		public PmdDataType CreateFromVersion(uint version, BinaryReader reader)
		{
			return version switch { 
				12=> new PmdData_P3Cutinfo(reader),
				_ => throw new NotImplementedException(),
			};


		}

		public PmdDataType? CreateType(uint version)
		{
			return version switch
			{
				12 => new PmdData_P3Cutinfo(),
				_ => throw new NotImplementedException(),
			};
		}

		public PmdDataType? ReadType(BinaryReader reader, uint version, List<PmdTypeID> typeIDs, PmdTypeFactory typeFactory)
		{
			var OriginalPos = reader.BaseStream.Position;

			reader.BaseStream.Position = OriginalPos + 0xC;
			reader.BaseStream.Position = (long)reader.ReadUInt32();

			var cut = CreateFromVersion(version, reader);
			reader.BaseStream.Position = OriginalPos;
			return cut;
		}
	}

	public class PmdData_P3Cutinfo : PmdDataType
	{
		public int FirstFrame { get; set; }
		public int LastFrame { get; set; }
		public int TotalFrame { get; set; }

		// Normally Id remove this but im not sure if its actually not used
		public int Reserve1 { get; set; }

		public int FieldMajorID { get; set; }
		public int FieldMinorID { get; set; }

		public short Unknown1 { get; set; }

		public short FieldFBN { get; set; }
		public int FieldENV { get; set; }

		public int Flags { get; set; }

		public PmdData_P3Cutinfo()
		{
			return;
		}
		public PmdData_P3Cutinfo(BinaryReader reader)
		{
			FirstFrame = reader.ReadInt32();
			LastFrame = reader.ReadInt32();
			TotalFrame = reader.ReadInt32();
			Reserve1 = reader.ReadInt32();
			FieldMajorID = reader.ReadInt32();
			FieldMinorID = reader.ReadInt32();
			Unknown1 = reader.ReadInt16();
			FieldFBN = reader.ReadInt16();
			FieldENV = reader.ReadInt32();
			Flags = reader.ReadInt32();
		}

		internal override void SaveData(PmdBuilder builder, BinaryWriter writer)
		{ 
			writer.Write(FirstFrame);
			writer.Write(LastFrame);
			writer.Write(TotalFrame);
			writer.Write(Reserve1);
			writer.Write(FieldMajorID);
			writer.Write(FieldMinorID);
			writer.Write(Unknown1);
			writer.Write(FieldFBN);
			writer.Write(FieldENV);
			writer.Write(Flags);
		}
		internal override int GetCount() => 1;
	}
}
