using System.IO;
using Newtonsoft.Json;
using LibellusLibrary.Converters;

namespace LibellusLibrary.PMD.Types
{
	public class Unknown : DataType, IVariableSize
	{
		public override DataTypeID TypeID => _typeID;
		private DataTypeID _typeID;

		[JsonConverter(typeof(ByteArrayToHexArray))]
		public byte[] Data;
		[JsonIgnore] public int DataSize;

		public Unknown() { }
		public Unknown(DataTypeID type) { _typeID = type; }
		public Unknown(string path, int size, DataTypeID type) { DataSize = size; Open(path); _typeID = type; }
		public Unknown(Stream stream, int size, DataTypeID type, bool leaveOpen = false) { DataSize = size; Open(stream, leaveOpen); _typeID = type; }
		public Unknown(BinaryReader reader, int size, DataTypeID type) { DataSize = size; Open(reader); _typeID = type; }

		internal override void Read(BinaryReader reader)
		{
			Data = reader.ReadBytes(DataSize);
			return;
		}

		internal override void Write(BinaryWriter writer)
		{
			writer.Write(Data);
		}

		public void SetType(DataTypeID type)
		{
			_typeID = type;
		}
	}
}
