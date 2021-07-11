using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;
using Yarhl.IO;

namespace JUSToolkit.Converters.Bin.SimpleBin
{
    public class Binary2SimpleBin : IConverter<BinaryFormat, Formats.Bin.SimpleBin>
    {
        public Formats.Bin.SimpleBin Convert(BinaryFormat source)
        {
            var simpleBin = new Formats.Bin.SimpleBin();
            var reader = new DataReader(source.Stream) {
                DefaultEncoding = Encoding.GetEncoding("shift_jis")
            };
            reader.Stream.Position = 0x00;  ///!!!!!!
            int textPos = reader.ReadInt32();
            reader.Stream.Position = 0x00;

            while(reader.Stream.Position < textPos) {
                simpleBin.text.Add(readPointerText(reader));
            }

            return simpleBin;
        }

        private string readPointerText(DataReader reader)
        {
            reader.Stream.PushToPosition(reader.Stream.Position + reader.ReadInt32());
            string text = reader.ReadString();
            reader.Stream.PopPosition();
            return string.IsNullOrEmpty(text) ? "<!empty>" : text;
        }
    }
}
