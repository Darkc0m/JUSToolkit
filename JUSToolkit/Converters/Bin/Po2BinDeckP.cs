using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;
using Yarhl.Media.Text;
using Yarhl.IO;
using JUSToolkit.Formats;

namespace JUSToolkit.Converters.Bin
{
    class Po2BinDeckP : IConverter<Po, BinDeckP>
    {
        public DataReader OriginalFile { get; set; }

        public BinDeckP Convert(Po source)
        {
            var bin = new BinDeckP();

            foreach(PoEntry entry in source.Entries)
                bin.Text.Add(entry.Text);

            OriginalFile.Stream.Position = 0x34;
            bin.code = OriginalFile.ReadBytes(0x0C);

            return bin;
        }
    }
}
