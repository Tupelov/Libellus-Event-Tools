using System;
using System.IO;

namespace LibellusLibrary.PMD.Types
{
	public static class TypeFactory
	{
		public static DataType CreateDataType(DataTypeID type)
		{
			Type dataType = GetDataType(type);
			return (DataType)Activator.CreateInstance(dataType);
		}

		public static DataType CreateDataType(DataTypeID type, BinaryReader reader, int size = 0)
		{
			Type dataType = GetDataType(type);
			if(dataType == typeof(Unknown))
			{
				return (DataType)Activator.CreateInstance(dataType, reader, size, type);
			}
			else
			{
				return (DataType)Activator.CreateInstance(dataType, reader);
			}
			
		}

		public static Type GetDataType(DataTypeID type)
		{
			Type dataType = type switch
			{
				DataTypeID.CutInfo => typeof(CutInfo),
				DataTypeID.Name => typeof(Name),
				//DataTypeID.Frame => typeof(PmdDataFrame),
				_ => typeof(Unknown)
			};
			return dataType;
		}
	}


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
}
