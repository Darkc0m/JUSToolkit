using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JUSToolkit.Converters.Bin.Koma
{
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
