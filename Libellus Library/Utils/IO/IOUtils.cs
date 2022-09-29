using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LibellusLibrary.Utils.IO
{
	public static class IOUtils
	{
		/// <summary>
		/// Makes reader seek to position.
		/// </summary>
		/// <param name="reader">This reader.</param>
		/// <param name="position">Position to seek to.</param>
		public static void FSeek(this BinaryReader reader, long position)
		{
			reader.BaseStream.Position = position;
		}

		public static void FSeek(this BinaryWriter writer, long position)
		{
			writer.BaseStream.Position = position;
		}

		public static long FTell(this BinaryReader reader)
		{
			return reader.BaseStream.Position;
		}

		public static long FTell(this BinaryWriter writer)
		{
			return writer.BaseStream.Position;
		}
		public static void FSkip(this BinaryReader reader, long position)
		{
			reader.BaseStream.Position += position;
		}
		public static void FSkip(this BinaryWriter writer, long position)
		{
			writer.BaseStream.Position += position;
		}

	}
}
