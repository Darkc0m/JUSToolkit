using JUSToolkit.Converters.Bin.ChrB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;

namespace JUSToolkit.Formats.Bin
{
    public class ChrB : JusFormat
    {

    }

    public class ChrBData : JusData
    {
        public string[] text;
        public int unk1, unk2, unk3;

        public ChrBData()
        {
            text = new string[51];
            for (int i = 0; i < text.Length; i++) {
                text[i] = readPointerText();
            }
            unk1 = reader.ReadInt16();
            unk2 = reader.ReadInt16();
            unk3 = reader.ReadInt16();
            reader.Stream.Position += 0x02;
        }
    }
}
