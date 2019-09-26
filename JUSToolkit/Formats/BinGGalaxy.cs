using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;

namespace JUSToolkit.Formats
{
    class BinGGalaxy : Format
    {
        public List<string> Text;
        public static int[] blockSize = { 0x3C, 0x48, 0x40};

        public BinGGalaxy()
        {
            Text = new List<string>();
        }
    }
}
