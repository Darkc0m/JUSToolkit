using System.Linq;
using Yarhl.FileFormat;
using Yarhl.Media.Text;
using Yarhl.IO;
using JUSToolkit.Formats;

namespace JUSToolkit.Converters.Bin
{
    class Po2BinDeck : IConverter<Po, BinDeck>
    {
        public DataReader OriginalFile { get; set; }

        public BinDeck Convert(Po source)
        {
            OriginalFile.Stream.Position = 0x00;

            var bin = new BinDeck
            {
                code = OriginalFile.ReadBytes(0x40)
            };

            bin.Text.Add(source.Entries.ElementAt(0).Text);

            return bin;
        }
    }
}
