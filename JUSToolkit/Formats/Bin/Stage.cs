using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;

namespace JUSToolkit.Formats.Bin
{
    public class Stage : JusFormat
    {

    }

    public class StageData : JusData
    {
        public string name;
        public int unk1, unk2, unk3;

        public static StageData readData()
        {
            var data = new StageData();
            data.name = readPointerText();
            data.unk1 = reader.ReadByte();
            data.unk2 = reader.ReadByte();
            data.unk3 = reader.ReadInt16();

            return data;
        }
    }
}
