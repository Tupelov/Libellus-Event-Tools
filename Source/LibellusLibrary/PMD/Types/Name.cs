using LibellusLibrary.Converters;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Linq;
using LibellusLibrary.Utils;

namespace LibellusLibrary.PMD.Types
{
	[DebuggerDisplay("Name: {String}")]
	public class Name : DataType
	{
		public override DataTypeID TypeID => DataTypeID.Name;

		public string String
		{
			get { return _string.Replace("\0", string.Empty); }
			set
			{
				if (value.Length > 32)
				{
					throw new System.Exception("Name can only be 32 characters!\nInputed String: " + value);
				}
				_string = value;
			}
		}

		private string _string;

	//	[JsonConverter(typeof(CharArrayToStringConverter))]
	//	public char[] Chars;

		public Name() { }
		public Name(string path) { Open(path); }
		public Name(Stream stream, bool leaveOpen = false) { Open(stream, leaveOpen); }
		public Name(BinaryReader reader) { Open(reader); }

		internal override void Read(BinaryReader reader)
		{
			byte[] temp = reader.ReadBytes(32);
			//Chars = temp;
			//
			String = Text.ASCII8ToString(temp);
			return;
		}
		internal override void Write(BinaryWriter writer)
		{
			byte[] temp = Text.StringtoASCII8(String);
			System.Array.Resize(ref temp, 32);
			writer.Write(temp);
		}
	}
}
