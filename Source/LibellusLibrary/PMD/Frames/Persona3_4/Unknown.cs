using System;
using System.IO;
using LibellusLibrary.Converters;
using Newtonsoft.Json;

namespace LibellusLibrary.PMD.Frames.Persona3_4
{
	public class Unknown : FrameInfo
	{
		[JsonConverter(typeof(ByteArrayToHexArray))]
		public byte[] Data;


		public Unknown() { }
		public Unknown(string path) { Open(path); }
		public Unknown(Stream stream, bool leaveOpen = false) { Open(stream, leaveOpen); }
		public Unknown(BinaryReader reader) { Open(reader); }


		internal override void Read(BinaryReader reader)
		{
			Data = reader.ReadBytes(52);
		}

		internal override void Write(BinaryWriter writer)
		{
			writer.Write(Data);
		}
	}
}
