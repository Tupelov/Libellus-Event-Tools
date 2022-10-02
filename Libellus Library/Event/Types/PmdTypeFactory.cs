using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibellusLibrary.Event.Types
{
	public class PmdTypeFactory
	{
		private List<PmdDataType> _types = new();
		public List<PmdTypeID> typeIDs = new();

		public List<PmdDataType> ReadDataTypes(BinaryReader reader, uint typeTableCount, uint version)
		{ 
			
			_types = new List<PmdDataType>();
			typeIDs = ReadTypes(reader, typeTableCount);
			foreach(var type in typeIDs)
			{
				ITypeCreator typecreator = GetTypeCreator(type);

				PmdDataType? dataType = typecreator.ReadType(reader, version, typeIDs,this);
				if(dataType != null) { 
					dataType.Type = type;
					_types.Add(dataType);
				}
				reader.BaseStream.Position += 0x10;
			}

			return _types;
		}

		public static ITypeCreator GetTypeCreator(PmdTypeID Type)=> Type switch
		{
			PmdTypeID.CutInfo => new PmdData_Cutinfo(),
			PmdTypeID.Unit => new PmdData_Unit(),
			PmdTypeID.FrameTable => new PmdData_FrameTable(),
			PmdTypeID.Message => new PmdData_Message(),
			_ => new UnkType()
		};

		public static bool IsSerialized(PmdTypeID type) => type switch
		{
			PmdTypeID.UnitData => false,
			PmdTypeID.Name => false,
			_=>true
		};

		private List<PmdTypeID> ReadTypes(BinaryReader reader, uint typeTableCount)
		{
			long originalpos = reader.BaseStream.Position;
			List<PmdTypeID> types = new List<PmdTypeID>();

			for(int i = 0; i < typeTableCount; i++)
			{
				types.Add((PmdTypeID)reader.ReadUInt32());
				reader.BaseStream.Position += 0x0c;
			}
			reader.BaseStream.Position = originalpos;
			return types;
		}
		public List<string> GetNameTable(BinaryReader reader)
		{
			var originalpos = reader.BaseStream.Position;
			var names = new List<string>();
			for(int i =0;i< typeIDs.Count; i++)
			{
				if(typeIDs[i] == PmdTypeID.Name)
				{
					reader.BaseStream.Position = 0x20 + (0x10 * i) +0x8;
					var nameCount = reader.ReadUInt32();
					reader.BaseStream.Position = reader.ReadUInt32();

					for(int j = 0;j < nameCount; j++)
					{
						string data = new(reader.ReadChars(32));
						names.Add(data.Replace("\0", string.Empty));
					}

				}
			}
			reader.BaseStream.Position = originalpos;
			return names;
		}
	}
}
