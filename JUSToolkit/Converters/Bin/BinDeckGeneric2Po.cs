using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;
using JUSToolkit.Formats;
using Yarhl.Media.Text;

namespace JUSToolkit.Converters.Bin
{
    class BinDeckGeneric2Po : IConverter<BinDeck, Po>
    {
        public Po Convert(BinDeck source)
        {
            Po poExport = new Po
            {
                Header = new PoHeader("Jump Ultimate Stars", "TranScene", "es")
                {
                    LanguageTeam = "TranScene",
                }
            };

            int i = 0;
            
            foreach(string entry in source.Text)
            {
                poExport.Add(new PoEntry(entry) { Context = source.Files[i] });
                i++;
            }

            return poExport;
        }
    }
}
