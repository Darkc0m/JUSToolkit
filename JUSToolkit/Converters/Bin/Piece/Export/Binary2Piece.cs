using JUSToolkit.Formats.Bin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;
using Yarhl.IO;

namespace JUSToolkit.Converters.Bin.Piece
{
    public class Binary2Piece : JusBinConverter, IConverter<BinaryFormat, Formats.Bin.Piece>
    {
        public Binary2Piece()
        {
            type = DataType.Piece;
        }
        public Formats.Bin.Piece Convert(BinaryFormat source)
        {
            return (Formats.Bin.Piece)transform(source, true);
        }
    }
}
