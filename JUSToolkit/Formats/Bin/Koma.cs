using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;

namespace JUSToolkit.Formats.Bin
{
    public class Koma : JusFormat
    {

    }

    public class KomaData : JusData
    {
        public string name;
        public int unk, num;

        public static KomaData readData()
        {
            var data = new KomaData();
            data.name = readPointerText();
            data.unk = reader.ReadInt32();
            data.num = reader.ReadInt32();

            return data;
        }
    }
}
