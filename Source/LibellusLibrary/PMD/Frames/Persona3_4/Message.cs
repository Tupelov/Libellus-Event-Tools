using System.IO;
using LibellusLibrary.Converters;
using Newtonsoft.Json;

namespace LibellusLibrary.PMD.Frames.Persona3_4
{
	internal class Message: FrameInfo
	{

		public byte[] Flags;
		public short MessageIndex;
		public short Field0E;
		public MessageMode Mode;
		public int Field14;
		public int Field18;
		public int Field1C;
		public int Field20;
		public int Field24;
		public int Field28;
		public int Field2C;
		public int Field30;

		public Message() { }
		public Message(string path) { Open(path); }
		public Message(Stream stream, bool leaveOpen = false) { Open(stream, leaveOpen); }
		public Message(BinaryReader reader) { Open(reader); }


		internal override void Read(BinaryReader reader)
		{
			Flags = reader.ReadBytes(12);
			MessageIndex = reader.ReadInt16();
			Field0E = reader.ReadInt16();
			Mode = (MessageMode)reader.ReadInt32();
			Field14 = reader.ReadInt32();
			Field18 = reader.ReadInt32();
			Field1C = reader.ReadInt32();
			Field20 = reader.ReadInt32();
			Field24 = reader.ReadInt32();
			Field28 = reader.ReadInt32();
			Field2C = reader.ReadInt32();
			Field30 = reader.ReadInt32();
		}

		internal override void Write(BinaryWriter writer)
		{
			writer.Write(Flags);
			writer.Write(MessageIndex);
			writer.Write(Field0E);
			writer.Write((int)Mode);
			writer.Write(Field14);
			writer.Write(Field18);
			writer.Write(Field1C);
			writer.Write(Field20);
			writer.Write(Field24);
			writer.Write(Field28);
			writer.Write(Field2C);
			writer.Write(Field30);
		}

		public enum MessageMode : int
		{ 
			Stop = 0,
			NoStop = 1
		}
	}
	
	/*
	 *
typedef struct {
	pmdDataInfo_t Data1[3];
	int MessageIndex;
	MESSAGE_MODE messageMode;
	pmdDataInfo_t Data2[8];
}pmdMessageObject_t;

	 */
}
