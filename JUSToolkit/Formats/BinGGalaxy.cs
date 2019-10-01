using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;

namespace JUSToolkit.Formats
{
    class BinGGalaxy : Bin
    {
        public static int[] blockSize = { 0x3C, 0x48, 0x40, 0x88};
        public List<List<byte[]>> code;
        public List<int> NumBlocks;
        public List<int> BlockStart;

        public BinGGalaxy()
        {
            code = new List<List<byte[]>>();
            NumBlocks = new List<int>();
            BlockStart = new List<int>();
        }
    }
}
