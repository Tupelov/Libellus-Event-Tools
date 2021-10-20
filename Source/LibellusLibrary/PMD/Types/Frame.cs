using System;
using System.IO;
using System.Linq;
using LibellusLibrary.Converters;
using LibellusLibrary.Json.Converters;
using LibellusLibrary.PMD.Frames;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace LibellusLibrary.PMD.Types
{

	//[JsonConverter(typeof(FrameInfo))]
	public class Frame : DataType, IVersioningHandler, IVersioning
	{
		[JsonIgnore] public override DataTypeID TypeID => DataTypeID.Frame;

		[JsonIgnore] public FormatVersion Version;

		Type IVersioningHandler.GetTypeFromVersion(FormatVersion version) { return GetTypeFromVersion(version); }
		public static Type GetTypeFromVersion(FormatVersion version)
		{
			if (GetSupportedVersions().Contains(version))
			{
				return typeof(Frame);
			}
			
			throw new System.NotImplementedException();
		}

		FormatVersion[] IVersioning.GetSupportedVersions() { return GetSupportedVersions(); }
		public static FormatVersion[] GetSupportedVersions()
		{
			return new FormatVersion[] {
					FormatVersion.v4_Nocturne,
					FormatVersion.v12_Persona3_4,
					FormatVersion.v13_Persona3_4
			};
		}
		



		[JsonConverter(typeof(StringEnumConverter))]
		public FrameInfoType InfoType;
		public ushort FrameStart;
		public ushort Length;
		public short NameIndex;
		[JsonConverter(typeof(DontDeserializeJsonConverter))]
		public FrameInfo FrameInfo;

		public Frame(FormatVersion version) { Version = version; }
		public Frame(string path, FormatVersion version) { Version = version; Open(path); }
		public Frame(Stream stream, FormatVersion version, bool leaveOpen = false) { Version = version; Open(stream, leaveOpen); }
		public Frame(BinaryReader reader, FormatVersion version) { Version = version; Open(reader); }

		internal override void Read(BinaryReader reader)
		{
			InfoType = (FrameInfoType)reader.ReadInt16();
			FrameStart = reader.ReadUInt16();
			Length = reader.ReadUInt16();
			NameIndex = reader.ReadInt16();
			FrameInfo = FrameInfoFactory.CreateFrameInfo(InfoType, Version, reader);
			return;
		}

		internal override void Write(BinaryWriter writer)
		{
			writer.Write((short)InfoType);
			writer.Write(FrameStart);
			writer.Write(Length);
			writer.Write(NameIndex);
			FrameInfo.Write(writer);
			return;
		}

		public static Frame ReadJson(JsonReader reader, JsonSerializer serializer, FormatVersion version)
		{
			var jsonObject = JObject.Load(reader);
			var frame = new Frame(version);
			serializer.Populate(jsonObject.CreateReader(), frame);
			//serializer.Populate(jsonObject["DataTable"].CreateReader(), data);
			
			Type infoType = (Type)Frames.FrameInfoFactory.GetVersionFrameInfoFactory(version).GetMethod("GetFrameInfoFromType", BindingFlags.Static | BindingFlags.Public).Invoke(null,new object[] { frame.InfoType});
			frame.FrameInfo = (FrameInfo)Activator.CreateInstance(infoType);
			serializer.Populate(jsonObject["FrameInfo"].CreateReader(), frame.FrameInfo);
			return frame;

		}
	}	

}
