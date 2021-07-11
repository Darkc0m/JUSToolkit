using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JUSToolkit.Converters.Bin.Location
{
    public class LocationData : JusData
    {
        public string name;
        public int unk1, unk2;

        public LocationData()
        {

        }

        public static LocationData readData()
        {
            var data = new LocationData();
            data.name = readPointerText();
            data.unk1 = reader.ReadInt16();
            data.unk2 = reader.ReadInt16();

            return data;
        }
    }
}
