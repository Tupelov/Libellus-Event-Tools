using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LibellusLibrary.PmdFile.DataTypes;

namespace LibellusLibrary.PmdFile.Common
{
	#region Enums
	public enum DataTypeID : int
	{
		CutInfo = 0,
		Name = 1,
		Stage = 2,
		Unit = 3,
		Frame = 4,
		Camera = 5,
		Message = 6,
		Effect = 7,
		EffectData = 8,
		UnitData = 9,
		F1 = 10,
		F2 = 11,
		FTB = 12,
		SLight = 13,
		SFog = 14,
		Blur2 = 15,
		MultBlur = 16,
		DistBlur = 17,
		Filter = 18,
		MultFilter = 19,
		RipBlur = 20,

		ObjectTable = 21,

		RainData = 25,
		BezierTable = 26,
		RefTable = 27,
		MAX = 28
	}


	public enum FrameObjectType: short
	{
		STAGE = 0,
		UNIT = 1,
		CAMERA = 2,
		EFFECT = 3,
		MESSAGE = 4,
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

	public enum FrameFlagType : short
	{
		Disable = 0,
		Local = 1,
		Global = 2,
	}

	public enum GlobalFlagType : short
	{
		Event = 0,
		Commu = 1,
		Sys = 2,
	}
	#endregion Enums


}
