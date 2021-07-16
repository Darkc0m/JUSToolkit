using JUSToolkit.Formats.Bin;
using Yarhl.FileFormat;
using Yarhl.Media.Text;

namespace JUSToolkit.Converters.Bin.Deck.Import
{
    public class Po2Deck : IConverter<Po, Formats.Bin.Deck>
    {
        public Formats.Bin.Deck Convert(Po source)
        {
            var deck = new Formats.Bin.Deck();

            foreach(PoEntry entry in source.Entries) {
                deck.data.Add(new DeckData(entry.Text));
            }

            return deck;
        }
    }
}
