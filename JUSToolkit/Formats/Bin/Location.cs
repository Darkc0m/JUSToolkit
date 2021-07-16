using System.Collections.Generic;
using Yarhl.FileFormat;

namespace JUSToolkit.Formats.Bin
{
    public class Location : IFormat
    {
        public List<LocationData> places;

        public Location()
        {
            places = new List<LocationData>();
        }
    }

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
