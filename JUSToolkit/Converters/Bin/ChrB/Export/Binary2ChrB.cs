using JUSToolkit.Formats.Bin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;
using Yarhl.IO;

namespace JUSToolkit.Converters.Bin.ChrB.Export
{
    public class Binary2ChrB : JusBinConverter, IConverter<BinaryFormat, Formats.Bin.ChrB>
    {
        public Binary2ChrB()
        {
            type = DataType.ChrB;
        }
        public Formats.Bin.ChrB Convert(BinaryFormat source)
        {
            return (Formats.Bin.ChrB)transform(source, false);
        }
    }
}
