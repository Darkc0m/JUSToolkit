using Yarhl.FileFormat;
using Yarhl.Media.Text;
using JUSToolkit.Formats;

namespace JUSToolkit.Converters.Bin
{
    class BinDeck2Po : IConverter<BinDeck, Po>
    {
        public Po Convert(BinDeck source)
        {
            Po po = new Po()
            {
                Header = new PoHeader("Jump Ultimate Stars", "TranScene", "es")
                {
                    LanguageTeam = "TranScene"
                }
            };

            po.Add(new PoEntry(source.text) { Context = "0", ExtractedComments = "Limite de 28 bytes" });

            return po;
        }
    }
}
