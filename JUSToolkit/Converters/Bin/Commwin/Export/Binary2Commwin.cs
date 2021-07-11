using JUSToolkit.Formats.Bin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;
using Yarhl.IO;

namespace JUSToolkit.Converters.Bin.Commwin.Export
{
    public class Binary2Commwin : JusBinConverter, IConverter<BinaryFormat, Formats.Bin.Commwin>
    {
        public Binary2Commwin()
        {
            type = DataType.Commwin;
        }

        public Formats.Bin.Commwin Convert(BinaryFormat source)
        {
            return (Formats.Bin.Commwin)transform(source, false);
        }
    }
}
