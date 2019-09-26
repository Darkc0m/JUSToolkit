using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;

namespace JUSToolkit.Formats
{
    class BinDeckP : Format
    {
        public List<string> Text;
        public byte[] code;

        public BinDeckP()
        {
            Text = new List<string>();
        }
    }
}
