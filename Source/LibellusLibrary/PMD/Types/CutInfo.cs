using System;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;

namespace LibellusLibrary.PMD.Types
{
	public abstract class CutInfo : DataType, IVersioningHandler
	{

		public override DataTypeID TypeID => DataTypeID.CutInfo;

		[JsonIgnore] public FormatVersion Version;
		
		public static Type GetTypeFromVersion(FormatVersion version)
		{
			if (CutinfoPersona3_4.GetSupportedVersions().Contains(version))
			{
				return typeof(CutinfoPersona3_4);
			}
			else if (CutinfoNocturne.GetSupportedVersions().Contains(version))
			{
				return typeof(CutinfoNocturne);
			}
			throw new System.NotImplementedException();
		}

		Type IVersioningHandler.GetTypeFromVersion(FormatVersion version)
		{
			return GetTypeFromVersion(version);
		}
		

	}
	public class CutinfoNocturne : CutInfo, IVersioning
	{

		public int FirstFrame;
		public int LastFrame;
		public int TotalFrame;
		public int Reserve1;

		public CutinfoNocturne() { }
		public CutinfoNocturne(string path, FormatVersion version) { Version = version; Open(path); }
		public CutinfoNocturne(Stream stream, FormatVersion version, bool leaveOpen = false) { Version = version; Open(stream, leaveOpen); }
		public CutinfoNocturne(BinaryReader reader, FormatVersion version) { Version = version; Open(reader); }

		internal override void Read(BinaryReader reader)
		{
			FirstFrame = reader.ReadInt32();
			LastFrame = reader.ReadInt32();
			TotalFrame = reader.ReadInt32();
			Reserve1 = reader.ReadInt32();
			return;
		}

		internal override void Write(BinaryWriter writer)
		{
			writer.Write(FirstFrame);
			writer.Write(LastFrame);
			writer.Write(TotalFrame);
			writer.Write(Reserve1);
			return;
		}

		FormatVersion[] IVersioning.GetSupportedVersions() 
		{
			return GetSupportedVersions(); 
		}


		public static FormatVersion[] GetSupportedVersions()
		{
			return new FormatVersion[] { 
				FormatVersion.v3_Nocturne, 
				FormatVersion.v4_Nocturne 
			};
		}

	}
	public class CutinfoPersona3_4 : CutInfo, IVersioning
	{

		FormatVersion[] IVersioning.GetSupportedVersions()
		{
			return GetSupportedVersions();
		}


		public static FormatVersion[] GetSupportedVersions()
		{
			return new FormatVersion[] {
					FormatVersion.v12_Persona3_4,
					FormatVersion.v13_Persona3_4
			};
		}

		
		public int FirstFrame;
		public int LastFrame;
		public int TotalFrame;
		public int Reserve1;
		public int FieldMajorNo;
		public int FieldMinorNo;
		public short Field18;
		public short FieldFBN;
		public int FieldENV;
		public int Field20;

		public CutinfoPersona3_4() {  }
		public CutinfoPersona3_4(string path, FormatVersion version) { Version = version; Open(path); }
		public CutinfoPersona3_4(Stream stream, FormatVersion version, bool leaveOpen = false) { Version = version; Open(stream, leaveOpen); }
		public CutinfoPersona3_4(BinaryReader reader,FormatVersion version) { Version = version; Open(reader); }

		internal override void Read(BinaryReader reader)
		{
			FirstFrame = reader.ReadInt32();
			LastFrame = reader.ReadInt32();
			TotalFrame = reader.ReadInt32();
			Reserve1 = reader.ReadInt32();
			FieldMajorNo = reader.ReadInt32();
			FieldMinorNo = reader.ReadInt32();
			Field18 = reader.ReadInt16();
			FieldFBN = reader.ReadInt16();
			FieldENV = reader.ReadInt32();
			Field20 = reader.ReadInt32();
			return;
		}

		internal override void Write(BinaryWriter writer)
		{
			writer.Write(FirstFrame);
			writer.Write(LastFrame);
			writer.Write(TotalFrame);
			writer.Write(Reserve1);
			writer.Write(FieldMajorNo);
			writer.Write(FieldMinorNo);
			writer.Write(Field18);
			writer.Write(FieldFBN);
			writer.Write(FieldENV);
			writer.Write(Field20);
			return;
		}
	}
}
