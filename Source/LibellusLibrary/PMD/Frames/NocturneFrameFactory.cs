using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibellusLibrary.PMD.Frames
{
	class NocturneFrameFactory
	{
		public static Type GetFrameInfoFromType(FrameInfoType infoType)
		{
			Type frameInfoType = infoType switch
			{
				_ => typeof(Nocturne.Unknown)
			};

			return frameInfoType;
		}
		public static FrameInfo CreateInfo(FrameInfoType infoType, BinaryReader reader=null)
		{
			Type frameInfoType = GetFrameInfoFromType(infoType);


			if (reader != null)
			{
				return (FrameInfo)Activator.CreateInstance(frameInfoType, reader);
			}
			else
			{
				return (FrameInfo)Activator.CreateInstance(frameInfoType);
			}
		}
	}
}
