using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using LibellusLibrary.JSON;
using LibellusLibrary.Utils;

namespace LibellusLibrary.Event.Types
{
	internal class PmdData_Message : PmdDataType, ITypeCreator, IExternalFile, IReferenceType
	{
		public string FileName { get; set; }


		public byte[] MessageData;

		public PmdDataType? CreateType(uint version)
		{
			return new PmdData_Message();
		}

		public PmdDataType? ReadType(BinaryReader reader, uint version, List<PmdTypeID> typeIDs, PmdTypeFactory typeFactory)
		{
			var OriginalPos = reader.BaseStream.Position;

			reader.BaseStream.Position = OriginalPos + 0x4;
			var size = reader.ReadUInt32();

			reader.BaseStream.Position = OriginalPos + 0x4;
			var count = reader.ReadUInt32();

			reader.BaseStream.Position = OriginalPos + 0xC;
			reader.BaseStream.Position = (long)reader.ReadUInt32();


			MessageData = reader.ReadBytes((int)size);
			FileName = "";
			foreach (string name in typeFactory.GetNameTable(reader))
			{
				if (name.EndsWith(".msg"))
				{
					FileName = name.Substring(0, name.LastIndexOf('.')) + ".bmd";
					break;
				}
				else if (name.EndsWith(".bmd"))
				{
					FileName = name;
					break;
				}
			}
			if (FileName == "")
			{
				FileName = "Message.bmd";
			}

			reader.BaseStream.Position = (long)OriginalPos;
			return this;
		}

		public async Task SaveExternalFile(string directory)
		{
			await File.WriteAllBytesAsync(Path.Combine(directory, FileName), MessageData);
		}

		public async Task LoadExternalFile(string directory)
		{
			MessageData = await File.ReadAllBytesAsync(directory + "\\" + FileName);
		}

		internal override void SaveData(PmdBuilder builder, BinaryWriter writer)
		{
			writer.Write(MessageData);
			return;
		}
		internal override int GetSize() => MessageData.Length;
		internal override int GetCount() => 1;

		public void SetReferences(PmdBuilder pmdBuilder)
		{
			byte[] temp = Text.StringtoASCII8(FileName);
			System.Array.Resize(ref temp, 32);
			pmdBuilder.AddReference(PmdTypeID.Name, temp);
		}
	}
}
