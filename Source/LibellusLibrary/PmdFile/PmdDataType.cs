using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using LibellusLibrary.IO;
using System.Diagnostics;

namespace LibellusLibrary.PmdFile
{
	public abstract class PmdDataType : FileBase
	{
		public int DataSize;
		//internal abstract override void Read(BinaryReader Read);
		//internal abstract override void Write(BinaryWriter writer);
	}

	public class PmdDataUnknown : PmdDataType
	{
		public byte[] Data;

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
			DataSize = 32;
			return;
		}
		internal override void Write(BinaryWriter writer)
		{
			writer.Write(Name);
		}
	}


}
