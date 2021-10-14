using System.Diagnostics;
using System.IO;

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

		public Name() { }
		public Name(string path) { Open(path); }
		public Name(Stream stream, bool leaveOpen = false) { Open(stream, leaveOpen); }
		public Name(BinaryReader reader) { Open(reader); }

		internal override void Read(BinaryReader reader)
		{
			String = new(reader.ReadChars(32));
			return;
		}
		internal override void Write(BinaryWriter writer)
		{
			char[] temp = String.ToCharArray();
			System.Array.Resize(ref temp, 32);
			writer.Write(temp);
		}
	}
}
