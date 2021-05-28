using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using LibellusLibrary.PmdFile.Common;
using LibellusLibrary.IO;

namespace LibellusLibrary.PmdFile.DataTypes.Frame
{

	public abstract class PmdFrameObject : FileBase
	{

		public static PmdFrameObject CreateFrameObject(FrameObjectType type, BinaryReader reader)
		{
			PmdFrameObject Entry = type switch
			{
				//DataTypeID.CutInfo => new PmdDataCutInfo(reader),
				//DataTypeID.Name => new PmdDataName(reader),
				_ => new PmdFrameUnknown(reader)
			};
			return Entry;

		}

	}

	public class PmdFrameUnknown : PmdFrameObject
	{

		byte[] Data;


		public PmdFrameUnknown(string path) { Open(path); }
		public PmdFrameUnknown(Stream stream, bool leaveOpen = false) { Open(stream, leaveOpen); }
		public PmdFrameUnknown(BinaryReader reader) { Open(reader); }


		internal override void Read(BinaryReader reader)
		{
			Data = reader.ReadBytes(52);
			return;
		}

		internal override void Write(BinaryWriter writer)
		{
			writer.Write(Data);
			return;
		}

	}
	public class PmdFrameFlag : FileBase
	{

		FrameFlagType Type;
		short FlagNo;
		short CmpValue;
		GlobalFlagType GFlagType;

		
		public PmdFrameFlag(string path) { Open(path); }
		public PmdFrameFlag(Stream stream, bool leaveOpen = false) { Open(stream, leaveOpen); }
		public PmdFrameFlag(BinaryReader reader) { Open(reader); }

		
		internal override void Read(BinaryReader reader)
		{
			Type = (FrameFlagType)reader.ReadInt16();
			FlagNo = reader.ReadInt16();
			CmpValue = reader.ReadInt16();
			if (Type == FrameFlagType.Global)
			{
				GFlagType = (GlobalFlagType)reader.ReadInt16();
			}

			return;
		}

		internal override void Write(BinaryWriter writer)
		{
			writer.Write((short)Type);
			writer.Write(FlagNo);
			writer.Write(CmpValue);
			if (Type == FrameFlagType.Global)
			{
				writer.Write((short)GFlagType);
			}
			else
			{
				writer.Write((short)0);
			}
			return;
		}

	}

}
