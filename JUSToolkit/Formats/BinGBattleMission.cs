using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;

namespace JUSToolkit.Formats
{
    class BinGBattleMission : Format
    {
        public List<string> Text;
        public static int blockSize = 0xA4;

        public BinGBattleMission()
        {
            Text = new List<string>();
        }
    }
}
