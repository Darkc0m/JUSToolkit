using JUSToolkit.Formats.Bin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;
using Yarhl.IO;

namespace JUSToolkit.Converters.Bin.Demo.Export
{
    public class Binary2Demo : JusBinConverter, IConverter<BinaryFormat, Formats.Bin.Demo>
    {
        public Binary2Demo()
        {
            type = DataType.Demo;
        }
        public Formats.Bin.Demo Convert(BinaryFormat source)
        {
            return (Formats.Bin.Demo)transform(source, true);
        }
    }
}
