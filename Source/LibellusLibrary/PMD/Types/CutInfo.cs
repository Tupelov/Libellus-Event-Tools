using System.IO;

namespace LibellusLibrary.PMD.Types
{
	public class CutInfo : DataType
	{
		public override DataTypeID TypeID => DataTypeID.CutInfo;


		public int FirstFrame;
		public int LastFrame;
		public int TotalFrame;
		public int Reserve1;
		public int FieldMajorNo;
		public int FieldMinorNo;
		public short Field18;
		public short FieldFBN;
		public int FieldENV;
		public int Field20;

		public CutInfo() { }
		public CutInfo(string path) { Open(path); }
		public CutInfo(Stream stream, bool leaveOpen = false) { Open(stream, leaveOpen); }
		public CutInfo(BinaryReader reader) { Open(reader); }

		internal override void Read(BinaryReader reader)
		{
			FirstFrame = reader.ReadInt32();
			LastFrame = reader.ReadInt32();
			TotalFrame = reader.ReadInt32();
			Reserve1 = reader.ReadInt32();
			FieldMajorNo = reader.ReadInt32();
			FieldMinorNo = reader.ReadInt32();
			Field18 = reader.ReadInt16();
			FieldFBN = reader.ReadInt16();
			FieldENV = reader.ReadInt32();
			Field20 = reader.ReadInt32();
			return;
		}

		internal override void Write(BinaryWriter writer)
		{
			writer.Write(FirstFrame);
			writer.Write(LastFrame);
			writer.Write(TotalFrame);
			writer.Write(Reserve1);
			writer.Write(FieldMajorNo);
			writer.Write(FieldMinorNo);
			writer.Write(Field18);
			writer.Write(FieldFBN);
			writer.Write(FieldENV);
			writer.Write(Field20);
			return;
		}

	}
	
}
