using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using LibellusLibrary.IO;
using System.Diagnostics;

namespace LibellusLibrary.PmdFile
{
    public class PmdFile : FileBase
    {
        //Header
        public byte FileType { get; set; }
        public byte FileFormat { get; set; }
        public short UserID { get; set; }
        public int FileSize { get; set; }
        public char[] MagicCode { get; set; }//PMD1/PMD2/PMD3
        public int ExpandSize { get; set; }
        public int TypeTableCount => TypeTable.Count;//Expands to get{return TypeTable.Count}
        public int Version { get; set; }
        public int Reserve2 { get; set; }
        public int Reserve3 { get; set; }

        List<PmdTypeTable> TypeTable { get; set; }


        public PmdFile(string path) { Open(path); }
        public PmdFile(Stream stream, bool leaveOpen = false) { Open(stream, leaveOpen); }
        public PmdFile(BinaryReader reader) { Open(reader); }


        internal override void Read(BinaryReader reader)
        {
            FileType = reader.ReadByte();
            FileFormat = reader.ReadByte();
            UserID = reader.ReadInt16();
            FileSize = reader.ReadInt32();
            MagicCode = reader.ReadChars(4);
            Console.WriteLine(MagicCode);
            ExpandSize = reader.ReadInt32();
            int typeTablecnt = reader.ReadInt32();
            Version = reader.ReadInt32();
            if (Version != 12)
            {
                Console.WriteLine("Warning: PMD version isn't supported");
            }
            Reserve2 = reader.ReadInt32();
            Reserve3 = reader.ReadInt32();
            TypeTable = new List<PmdTypeTable>();

            for (int i = 0; i < typeTablecnt; i++)
            {
                TypeTable.Add(new PmdTypeTable(reader));
            }
            return;

        }

        internal override void Write(BinaryWriter writer)
        {

        }
    }

    [DebuggerDisplay("Type: {Type}")]
    public class PmdTypeTable : FileBase
    {

        DataTypes Type { get; set; }
        int ItemSize { get; set; }
        int ItemCount => DataTable.Count;
        int ItemAddress { get; set; }

        List<PmdDataType> DataTable { get; set; }

        public PmdTypeTable(string path) { Open(path); }
        public PmdTypeTable(Stream stream, bool leaveOpen = false) { Open(stream, leaveOpen); }
        public PmdTypeTable(BinaryReader reader) { Open(reader); }


        internal override void Read(BinaryReader reader)
        {
            Type = (DataTypes)reader.ReadInt32();
            ItemSize = reader.ReadInt32();
            int dataCount = reader.ReadInt32();
            ItemAddress = reader.ReadInt32();
            DataTable = new List<PmdDataType>();

            for(int i=0; i < dataCount; i++)
            {
                long currentPos = reader.FTell();
                reader.FSeek(ItemAddress);
                PmdDataType Entry = Type switch { 
                DataTypes.Name => new PmdDataName(reader),
                _              => new PmdDataUnknown(reader, ItemSize)
                };
                DataTable.Add(Entry);
                reader.FSeek(currentPos);
            }

        }

        internal override void Write(BinaryWriter writer)
        {
        }

        enum DataTypes : int
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


}
