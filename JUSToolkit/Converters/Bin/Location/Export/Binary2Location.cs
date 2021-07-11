using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;
using Yarhl.IO;

namespace JUSToolkit.Converters.Bin.Location.Export
{
    public class Binary2Location : IConverter<BinaryFormat, Formats.Bin.Location>
    {
        public Formats.Bin.Location Convert(BinaryFormat source)
        {
            var location = new Formats.Bin.Location();
            var reader = new DataReader(source.Stream) {
                DefaultEncoding = Encoding.GetEncoding("shift_jis")
            };
            JusData.reader = reader;
            
            int numPlaces = reader.ReadInt32();
            for (int i = 0; i < numPlaces; i++) {
                location.places.Add(LocationData.readData());
            }

            return location;
        }
    }
}
