using System.Diagnostics;
using System.IO;

using Newtonsoft.Json;

namespace LibellusLibrary.PMD.Types
{
	
	public class Message : DataType, IVariableSize, IExternalFile
	{
		public override DataTypeID TypeID => DataTypeID.Name;

		[JsonIgnore] public byte[] Data { get; set; }
		[JsonIgnore] public int DataSize { get; set; }
		public int NameIndex { get => 0; set { } }
		
		public Message() { }
		public Message(string path) { Open(path); }
		public Message(Stream stream, bool leaveOpen = false) { Open(stream, leaveOpen); }
		public Message(BinaryReader reader, int size) { DataSize = size; Open(reader); }

		internal override void Read(BinaryReader reader)
		{
			Data = reader.ReadBytes(DataSize);
			return;
		}
		internal override void Write(BinaryWriter writer)
		{
			writer.Write(Data);
		}

		// No decompiling support yet so this will have to do
		public void LoadFile(string dir, string file)
		{
			file = file.Replace("msg", "bmd");
			Data = File.ReadAllBytes(dir + Path.DirectorySeparatorChar + file);
		}

		public void SaveFile(string dir, string file)
		{
			file = file.Replace("msg", "bmd");
			File.WriteAllBytes(dir + Path.DirectorySeparatorChar + file, Data);
		}
	}
}
