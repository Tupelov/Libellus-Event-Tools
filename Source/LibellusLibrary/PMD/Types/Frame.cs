using System.IO;

using LibellusLibrary.Converters;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LibellusLibrary.PMD.Types
{
	public class Frame : DataType
	{
		public override DataTypeID TypeID => DataTypeID.Frame;

		[JsonConverter(typeof(StringEnumConverter))]
		public FrameInfoType InfoType;
		public ushort FrameStart;
		public ushort Length;
		public short NameIndex;
		//public PmdFrameObject Object;
		[JsonConverter(typeof(ByteArrayToHexArray))]
		public byte[] FrameInfo;

		public Frame() { }
		public Frame(string path) { Open(path); }
		public Frame(Stream stream, bool leaveOpen = false) { Open(stream, leaveOpen); }
		public Frame(BinaryReader reader) { Open(reader); }

		internal override void Read(BinaryReader reader)
		{
			InfoType = (FrameInfoType)reader.ReadInt16();
			FrameStart = reader.ReadUInt16();
			Length = reader.ReadUInt16();
			NameIndex = reader.ReadInt16();
			FrameInfo = reader.ReadBytes(52);
			return;
		}

		internal override void Write(BinaryWriter writer)
		{
			writer.Write((short)InfoType);
			writer.Write(FrameStart);
			writer.Write(Length);
			writer.Write(NameIndex);
			writer.Write(FrameInfo);
			return;
		}

	}

	public enum FrameInfoType : short
	{
		STAGE = 0,
		UNIT = 1,
		CAMERA = 2,
		EFFECT = 3,
		MESSAGE = 4,
		SE = 5,
		FADE = 6,
		QUAKE = 7,
		BLUR = 8,
		LIGHT = 9,
		SLIGHT = 10,
		SFOG = 11,
		SKY = 12,
		BLUR2 = 13,
		MBLUR = 14,
		DBLUR = 15,
		FILTER = 16,
		MFILTER = 17,
		BED = 18,
		BGM = 19,
		MG1 = 20,
		MG2 = 21,
		FB = 22,
		RBLUR = 23,

		TMX = 24,

		EPL = 26,
		HBLUR = 27,
		PADACT = 28,
		MOVIE = 29,
		TIMEI = 30,
		RENDERTEX = 31,
		BISTA = 32,
		CTLCAM = 33,
		WAIT = 34,
		B_UP = 35,
		CUTIN = 36,
		EVENT_EFFECT = 37,
		JUMP = 38,
		KEYFREE = 39,
		RANDOMJUMP = 40,
		CUSTOMEVENT = 41,
		CONDJUMP = 42,
		COND_ON = 43,
		COMULVJUMP = 44,
		COUNTJUMP = 45,
		HOLYJUMP = 46,
		FIELDOBJ = 47,
		PACKMODEL = 48,
		FIELDEFF = 49,
		SPUSE = 50,
		SCRIPT = 51,
		BLURFILTER = 52,
		FOG = 53,
		ENV = 54,
		FLDSKY = 55,
		FLDNOISE = 56,
		CAMERA_STATE = 57
	}

}
