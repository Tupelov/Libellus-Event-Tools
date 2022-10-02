using LibellusLibrary.Event.Types.Frame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LibellusLibrary.Event.Types
{
	internal class PmdData_FrameTable : PmdDataType, ITypeCreator
	{

		[JsonConverter(typeof(PmdFrameReader))]
		public List<PmdTargetType> Frames { get; set; }

		public PmdDataType? CreateType(uint version)
		{
			return version switch
			{
				12 => new PmdData_FrameTable(),
				_ => new PmdData_RawData()
			};
		}

		public PmdDataType? ReadType(BinaryReader reader, uint version, List<PmdTypeID> typeIDs, PmdTypeFactory typeFactory)
		{
			var OriginalPos = reader.BaseStream.Position;

			reader.BaseStream.Position = OriginalPos + 0x4;
			var size = reader.ReadUInt32();

			reader.BaseStream.Position = OriginalPos + 0x8;
			var count = reader.ReadUInt32();

			reader.BaseStream.Position = OriginalPos + 0xC;
			reader.BaseStream.Position = (long)reader.ReadUInt32();

			PmdFrameFactory factory = new();
			Frames = factory.ReadDataTypes(reader, count);

			reader.BaseStream.Position = OriginalPos;
			return this;
		}

		internal override void SaveData(PmdBuilder builder, BinaryWriter writer)
		{
			foreach(PmdTargetType frame in Frames)
			{
				frame.WriteFrame(writer);
			}
		}
		internal override int GetCount() => Frames.Count;
		internal override int GetSize() => 0x3c; // Data size doesnt matter for certain types

	}


}
