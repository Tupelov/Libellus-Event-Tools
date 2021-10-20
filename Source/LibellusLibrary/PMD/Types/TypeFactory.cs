using System;
using System.IO;
using System.Linq;
using System.Reflection;

using LibellusLibrary.Utils;

namespace LibellusLibrary.PMD.Types
{
	public static class TypeFactory
	{
		public static DataType CreateDataType(DataTypeID type, FormatVersion version)
		{
			Type dataType = GetDataType(type, version);
			return (DataType)Activator.CreateInstance(dataType);
		}

		public static DataType CreateDataType(DataTypeID type, BinaryReader reader, FormatVersion version, int size = 0)
		{
			Type dataType = GetDataType(type, version);
			if(dataType.HasInterface(typeof(IVariableSize)))
			{
				if (dataType == typeof(Unknown))
				{
					return (DataType)Activator.CreateInstance(dataType, reader, size, type);
				}
				return (DataType)Activator.CreateInstance(dataType, reader, size);
			}else if (dataType.HasInterface(typeof(IVersioning)))
			{
				return (DataType)Activator.CreateInstance(dataType, reader, version);
			}
			else
			{
				return (DataType)Activator.CreateInstance(dataType, reader);
			}
			
		}

		public static Type GetDataType(DataTypeID type, FormatVersion version)
		{
			Type dataType = type switch
			{
				DataTypeID.CutInfo => typeof(CutInfo),
				DataTypeID.Name => typeof(Name),
				DataTypeID.Frame => typeof(Frame),
				DataTypeID.Message => typeof(Message),
				_ => typeof(Unknown)
			};
			if (dataType.HasInterface(typeof(IVersioningHandler)))
			{
				MethodInfo method = dataType.GetMethod("GetTypeFromVersion", BindingFlags.Static | BindingFlags.Public);
				object[] args = { version };
				dataType = (Type)method.Invoke(null, args);
			}
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
