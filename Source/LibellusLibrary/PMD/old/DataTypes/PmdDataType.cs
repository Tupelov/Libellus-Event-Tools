/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using LibellusLibrary.IO;
using System.Diagnostics;


namespace LibellusLibrary.PmdFile.DataTypes
{

	public abstract class PmdDataType : FileBase
	{



	}

	public class PmdDataUnknown : PmdDataType
	{
		public byte[] Data;
		public int DataSize;

		public PmdDataUnknown() { }
		public PmdDataUnknown(string path, int size) { DataSize = size; Open(path); }
		public PmdDataUnknown(Stream stream, int size, bool leaveOpen = false) { DataSize = size; Open(stream, leaveOpen); }
		public PmdDataUnknown(BinaryReader reader, int size) { DataSize = size; Open(reader); }

		internal override void Read(BinaryReader reader)
		{
			Data = reader.ReadBytes(DataSize);
			return;
		}

		internal override void Write(BinaryWriter writer)
		{
			writer.Write(Data);
		}
	}

	public class PmdDataCutInfo : PmdDataType
	{

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

		public PmdDataCutInfo() { }
		public PmdDataCutInfo(string path) { Open(path); }
		public PmdDataCutInfo(Stream stream, bool leaveOpen = false) { Open(stream, leaveOpen); }
		public PmdDataCutInfo(BinaryReader reader) { Open(reader); }

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

	[DebuggerDisplay("Name: {DebuggerDisplay}")]
	public class PmdDataName : PmdDataType
	{

		private string DebuggerDisplay { get { return new string(Name); } }


		public char[] Name;

		public PmdDataName(string path) { Open(path); }
		public PmdDataName(Stream stream, bool leaveOpen = false) { Open(stream, leaveOpen); }
		public PmdDataName(BinaryReader reader) { Open(reader); }

		internal override void Read(BinaryReader reader)
		{
			Name = reader.ReadChars(32);
			return;
		}
		internal override void Write(BinaryWriter writer)
		{
			writer.Write(Name);
		}
	}

	public class PmdDataUnitTable : FileBase
	{
		public int NameIndex;
		public int FileIndex;
		public int MajorNum;
		public int MinorNum;
		public int DataSize;
		public int Unknown1;
		public int Reserve1;


		public PmdDataUnitTable(string path) { Open(path); }
		public PmdDataUnitTable(Stream stream, bool leaveOpen = false) { Open(stream, leaveOpen); }
		public PmdDataUnitTable(BinaryReader reader) { Open(reader); }

		internal override void Read(BinaryReader reader)
		{
			NameIndex = reader.ReadInt32();
			FileIndex = reader.ReadInt32();
			MajorNum = reader.ReadInt32();

			return;
		}

		internal override void Write(BinaryWriter writer)
		{
			return;
		}
	}
	
	[DebuggerDisplay("Type: {ObjectType}")]
	public class PmdDataFrame : PmdDataType
	{
		public FrameObjectType ObjectType;
		public ushort Frame;
		public ushort Length;
		public short NameIndex;
		public PmdFrameObject Object;

		public PmdDataFrame() { }
		public PmdDataFrame(string path) { Open(path); }
		public PmdDataFrame(Stream stream, bool leaveOpen = false) { Open(stream, leaveOpen); }
		public PmdDataFrame(BinaryReader reader) { Open(reader); }

		internal override void Read(BinaryReader reader)
		{
			ObjectType = (FrameObjectType)reader.ReadInt16();
			Frame = reader.ReadUInt16();
			Length = reader.ReadUInt16();
			NameIndex = reader.ReadInt16();
			Object = PmdFrameObject.CreateFrameObject(ObjectType, reader);
			return;
		}

		internal override void Write(BinaryWriter writer)
		{
			writer.Write((short)ObjectType);
			writer.Write(Frame);
			writer.Write(Length);
			writer.Write(NameIndex);
			Object.Write(writer);
			return;
		}

	}

}
*/