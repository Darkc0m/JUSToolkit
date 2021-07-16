using JUSToolkit.Converters.Bin.Rulemess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;

namespace JUSToolkit.Formats.Bin
{
    public class Rulemess : JusFormat
    {

    }

    public class RulemessData : JusData
    {
        public string text;
        public int unk;

        public RulemessData()
        {
            text = string.Empty;
        }

        public static RulemessData readData()
        {
            var data = new RulemessData();
            for (int i = 0; i < 3; i++) {
                data.text += $"{readPointerText(false)}\n";
            }
            data.unk = reader.ReadInt32();

            return data;
        }
    }
}
