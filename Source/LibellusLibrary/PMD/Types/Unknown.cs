using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using LibellusLibrary.IO;
using LibellusLibrary.Converters;

namespace LibellusLibrary.PMD.Types
{
	public class Unknown : DataType
	{
		[JsonConverter(typeof(ByteArrayToHexArray))]
		public byte[] Data;
		DataTypeID TypeID;
		[JsonIgnore] public int DataSize;

		public Unknown() { }
		public Unknown(DataTypeID type) { }
		public Unknown(string path, int size, DataTypeID type) { DataSize = size; Open(path); TypeID = type; }
		public Unknown(Stream stream, int size, DataTypeID type, bool leaveOpen = false) { DataSize = size; Open(stream, leaveOpen); TypeID = type; }
		public Unknown(BinaryReader reader, int size, DataTypeID type) { DataSize = size; Open(reader); TypeID = type; }

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
}
