using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JUSToolkit.Converters.Bin.Bgm
{
    public class BgmData : JusData
    {
        public string title;
        public string description;
        public int unk1, unk2, icon;

        public BgmData()
        {
            description = string.Empty;
            title = readPointerText();
            for (int i = 0; i < 3; i++) {
                description += $"{readPointerText(false)}\n";
            }
            unk1 = reader.ReadInt16();
            unk2 = reader.ReadInt16();
            icon = reader.ReadInt32();
        }

        //public static BgmData readData()
        //{
        //    var data = new BgmData();
        //    data.title = readPointerText();
        //    for(int i = 0; i < data.description.Length; i++) {
        //        data.description += $"{readPointerText(false)}\n";
        //    }
        //    data.unk1 = reader.ReadInt16();
        //    data.unk2 = reader.ReadInt16();
        //    data.icon = reader.ReadInt32();

        //    return data;
        //}
    }
}
