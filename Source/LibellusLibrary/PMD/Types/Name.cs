using System.Diagnostics;
using System.IO;
using System.Linq;

namespace LibellusLibrary.PMD.Types
{
	[DebuggerDisplay("Name: {Name}")]
	public class Name : DataType
	{


		public string String { 
			get { return new string(_Name).Replace("\0",string.Empty); } 
			set {
				_Name = value.ToCharArray().Take(32).ToArray(); 
			}  }

		private char[] _Name;

		public Name() { }
		public Name(string path) { Open(path); }
		public Name(Stream stream, bool leaveOpen = false) { Open(stream, leaveOpen); }
		public Name(BinaryReader reader) { Open(reader); }

		internal override void Read(BinaryReader reader)
		{
			_Name = reader.ReadChars(32);
			return;
		}
		internal override void Write(BinaryWriter writer)
		{
			char[] temp = _Name;
			System.Array.Resize(ref temp, 32);
			writer.Write(temp);
		}
	}
}
