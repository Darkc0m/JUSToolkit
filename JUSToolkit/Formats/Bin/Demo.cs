using JUSToolkit.Converters.Bin.Demo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;

namespace JUSToolkit.Formats.Bin
{
    public class Demo : JusFormat
    {

    }

    public class DemoData : JusData
    {
        public string title;
        public string[] description;
        public int id, icon;

        public DemoData()
        {
            description = new string[3];
            title = readPointerText();
            for (int i = 0; i < description.Length; i++) {
                description[i] = readPointerText();
            }
            id = reader.ReadByte();
            icon = reader.ReadByte();
        }
    }
}
