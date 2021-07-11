using JUSToolkit.Formats.Bin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;
using Yarhl.IO;

namespace JUSToolkit.Converters.Bin.Deck.Export
{
    public class Binary2Deck : JusBinConverter, IConverter<BinaryFormat, Formats.Bin.Deck>
    {
        public Binary2Deck()
        {
            type = DataType.Deck;
        }
        public Formats.Bin.Deck Convert(BinaryFormat source)
        {
            return (Formats.Bin.Deck)transform(source, false);
        }
    }
}
