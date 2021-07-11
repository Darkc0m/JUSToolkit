using JUSToolkit.Formats.Bin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;
using Yarhl.IO;

namespace JUSToolkit.Converters.Bin.Info.Export
{
    public class Binary2Info : JusBinConverter, IConverter<BinaryFormat, Formats.Bin.Info>
    {
        public Binary2Info()
        {
            type = DataType.Info;
        }
        public Formats.Bin.Info Convert(BinaryFormat source)
        {
            return (Formats.Bin.Info)transform(source, false);
        }
    }
}
