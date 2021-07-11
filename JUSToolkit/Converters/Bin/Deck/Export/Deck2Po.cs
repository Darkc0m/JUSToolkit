using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;
using Yarhl.Media.Text;

namespace JUSToolkit.Converters.Bin.Deck.Export
{
    public class Deck2Po : IConverter<Formats.Bin.Deck, Po>
    {
        public Po Convert(Formats.Bin.Deck source)
        {
            var po = new Po {
                Header = new PoHeader("Jump Ultimate Stars", "TranScene", "es") {
                    LanguageTeam = "TranScene",
                }
            };

            for(int i = 0; i < source.data.Count; i++) {
                po.Add(new PoEntry(((DeckData)source.data[i]).text) {
                    Context = $"{i}"
                });
            }

            return po;
        }
    }
}
