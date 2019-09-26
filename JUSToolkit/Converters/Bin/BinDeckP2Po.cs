using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;
using Yarhl.Media.Text;
using JUSToolkit.Formats;

namespace JUSToolkit.Converters.Bin
{
    class BinDeckP2Po : IConverter<BinDeckP, Po>
    {
        public Po Convert(BinDeckP source)
        {
            Po po = new Po()
            {
                Header = new PoHeader("Jump Ultimate Stars", "TranScene", "es")
                {
                    LanguageTeam = "TranScene"
                }
            };

            po.Add(new PoEntry(source.Text.ElementAt(0)) { Context = "0", ExtractedComments = "Limite de 10 caracteres" });
            po.Add(new PoEntry(source.Text.ElementAt(1)) { Context = "1", ExtractedComments = "Limite de 32 bytes" });

            return po;
        }
    }
}
