﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibellusLibrary.PMD.Frames.Persona3_4;
namespace LibellusLibrary.PMD.Frames
{
	public class Persona3_4FrameFactory
	{
		public static Type GetFrameInfoFromType(FrameInfoType infoType)
		{
			Type frameInfoType = infoType switch
			{
				FrameInfoType.Message=> typeof(Message),
				_ => typeof(Unknown)
			};

			return frameInfoType;
		}
		public static FrameInfo CreateInfo(FrameInfoType infoType, BinaryReader reader = null)
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