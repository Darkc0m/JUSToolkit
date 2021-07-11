using JUSToolkit.Converters.Bin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;

namespace JUSToolkit.Formats.Bin
{
    public class JusFormat : IFormat
    {
        public List<JusData> data;

        public JusFormat()
        {
            data = new List<JusData>();
        }
    }
}
