using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LibellusLibrary.PMD.Frames
{

	public static class FrameInfoFactory
	{
		public static Type GetVersionFrameInfoFactory(FormatVersion version)
		{
			Type versionFrameInfoFactory = version switch
			{
				FormatVersion.v4_Nocturne => typeof(NocturneFrameFactory),
				FormatVersion.v12_Persona3_4 => typeof(Persona3_4FrameFactory),
				FormatVersion.v13_Persona3_4 => typeof(Persona3_4FrameFactory),
				_ => throw new NotImplementedException(),
			};
			return versionFrameInfoFactory;
		}

		public static FrameInfo CreateFrameInfo(FrameInfoType infoType, FormatVersion version, BinaryReader reader = null)
		{
			Type factoryType = GetVersionFrameInfoFactory(version);

			MethodInfo method = factoryType.GetMethod("CreateInfo", BindingFlags.Static | BindingFlags.Public);
			object[] args = new object[] { infoType, reader };
			return (FrameInfo)method.Invoke(null, args);
		}

	}

	public enum FrameInfoType : short
	{
		STAGE = 0,
		UNIT = 1,
		CAMERA = 2,
		EFFECT = 3,
		Message = 4,
		SE = 5,
		FADE = 6,
		QUAKE = 7,
		BLUR = 8,
		LIGHT = 9,
		SLIGHT = 10,
		SFOG = 11,
		SKY = 12,
		BLUR2 = 13,
		MBLUR = 14,
		DBLUR = 15,
		FILTER = 16,
		MFILTER = 17,
		BED = 18,
		BGM = 19,
		MG1 = 20,
		MG2 = 21,
		FB = 22,
		RBLUR = 23,

		TMX = 24,

		EPL = 26,
		HBLUR = 27,
		PADACT = 28,
		MOVIE = 29,
		TIMEI = 30,
		RENDERTEX = 31,
		BISTA = 32,
		CTLCAM = 33,
		WAIT = 34,
		B_UP = 35,
		CUTIN = 36,
		EVENT_EFFECT = 37,
		JUMP = 38,
		KEYFREE = 39,
		RANDOMJUMP = 40,
		CUSTOMEVENT = 41,
		CONDJUMP = 42,
		COND_ON = 43,
		COMULVJUMP = 44,
		COUNTJUMP = 45,
		HOLYJUMP = 46,
		FIELDOBJ = 47,
		PACKMODEL = 48,
		FIELDEFF = 49,
		SPUSE = 50,
		SCRIPT = 51,
		BLURFILTER = 52,
		FOG = 53,
		ENV = 54,
		FLDSKY = 55,
		FLDNOISE = 56,
		CAMERA_STATE = 57
	}
}
