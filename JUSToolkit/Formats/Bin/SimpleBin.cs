using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;

namespace JUSToolkit.Formats.Bin
{
    public class SimpleBin : IFormat
    {
        public List<string> text;

        public SimpleBin()
        {
            text = new List<string>();
        }
    }
}
