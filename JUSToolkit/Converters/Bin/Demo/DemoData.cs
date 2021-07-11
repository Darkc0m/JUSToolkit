using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JUSToolkit.Converters.Bin.Demo
{
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
