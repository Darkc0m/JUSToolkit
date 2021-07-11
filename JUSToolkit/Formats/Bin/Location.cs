using JUSToolkit.Converters.Bin.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
}
