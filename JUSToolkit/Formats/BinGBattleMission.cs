using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;

namespace JUSToolkit.Formats
{
    class BinGBattleMission : Bin
    {
        public static int blockSize = 0xA4;
        public List<byte[]> code;

        public BinGBattleMission()
        {
            code = new List<byte[]>();
        }
    }
}
