using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;
using Yarhl.IO;
using JUSToolkit.Formats;

namespace JUSToolkit.Converters.Bin
{
    class BinDeck2Binary : IConverter<BinDeck, BinaryFormat>
    {
        public BinaryFormat Convert(BinDeck source)
        {
            var bin = new BinaryFormat();

            DataWriter writer = new DataWriter(bin.Stream);

            writer.Write(source.code);
            writer.Write(source.Text.ElementAt(0), false, Encoding.GetEncoding(932));
            while(writer.Stream.Position <= 0x5B)
            {
                writer.WriteOfType<byte>(0x00);
            }

            return bin;
        }
    }
}
