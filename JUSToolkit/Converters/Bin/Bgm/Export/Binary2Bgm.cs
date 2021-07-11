using JUSToolkit.Formats.Bin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;
using Yarhl.IO;

namespace JUSToolkit.Converters.Bin.Bgm.Export
{
    public class Binary2Bgm : JusBinConverter, IConverter<BinaryFormat, Formats.Bin.Bgm>
    {
        public Binary2Bgm()
        {
            type = DataType.Bgm;
        }
        public Formats.Bin.Bgm Convert(BinaryFormat source)
        {
            return (Formats.Bin.Bgm)transform(source, true);
        }
    }
}
