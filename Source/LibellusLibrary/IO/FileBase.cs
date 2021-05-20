using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LibellusLibrary.IO
{
	public abstract class FileBase
	{

		internal abstract void Read(BinaryReader reader);
		internal abstract void Write(BinaryWriter writer);

		public void Save(string path)
		{
			using (BinaryWriter writer = new BinaryWriter(File.Create(path)))
			{
				Write(writer);
			}
		}

		public void Save(Stream stream, bool leaveOpen = false)
		{
			using (BinaryWriter writer = new BinaryWriter(stream, Encoding.Default, leaveOpen))
			{
				Write(writer);
			}
		}

		public void Save(BinaryWriter writer)
		{
			Write(writer);

		}

		public void Open(string path)
		{
			//Use a memory stream instead of using a file stream
			using (MemoryStream stream = new MemoryStream(File.ReadAllBytes(path)))
			{
				Open(stream, false);
			}
		}

		public void Open(Stream stream, bool leaveOpen = false)
		{
			using (BinaryReader reader = new BinaryReader(stream, Encoding.Default, leaveOpen))
			{
				Open(reader);
			}
		}

		public void Open(BinaryReader reader)
		{
			Read(reader);
		}

	}
}
