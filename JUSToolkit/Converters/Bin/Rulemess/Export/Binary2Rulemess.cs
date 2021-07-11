using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;
using Yarhl.IO;

namespace JUSToolkit.Converters.Bin.Rulemess.Export
{
    public class Binary2Rulemess : IConverter<BinaryFormat, Formats.Bin.Rulemess>
    {
        public Formats.Bin.Rulemess Convert(BinaryFormat source)
        {
            var rulemess = new Formats.Bin.Rulemess();
            var reader = new DataReader(source.Stream) {
                DefaultEncoding = Encoding.GetEncoding("shift_jis")
            };
            JusData.reader = reader;

            reader.Stream.Position = 0x00;
            int firstText = reader.ReadInt32();
            reader.Stream.Position = 0x00;

            while (reader.Stream.Position < firstText) {
                rulemess.data.Add(RulemessData.readData());
            }

            return rulemess;
        }
    }
}
