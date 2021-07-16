using JUSToolkit.Converters.Bin.Commwin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;

namespace JUSToolkit.Formats.Bin
{
    public class Commwin : JusFormat
    {

    }

    public class CommwinData : JusData
    {
        public int unk;
        public string[] text;

        public CommwinData()
        {
            text = new string[8];
            unk = reader.ReadInt32();
            for (int i = 0; i < text.Length; i++) {
                text[i] = readPointerText();
            }
        }
    }
}
