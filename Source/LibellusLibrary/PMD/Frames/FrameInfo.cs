using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LibellusLibrary.IO;

namespace LibellusLibrary.PMD.Frames
{
	public abstract class FrameInfo : FileBase
	{

		public FrameInfo() { }
		public FrameInfo(string path) { Open(path); }
		public FrameInfo(Stream stream, bool leaveOpen = false) { Open(stream, leaveOpen); }
		public FrameInfo(BinaryReader reader) { Open(reader); }


	}

}
