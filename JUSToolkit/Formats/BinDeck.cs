using Yarhl.FileFormat;
using System.Collections.Generic;

namespace JUSToolkit.Formats
{
    class BinDeck : Bin
    {
        public List<string> Files;
        public byte[] code;

        public BinDeck()
        {
            Files = new List<string>();
        }
    }
}
