using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;
using Yarhl.Media.Text;

namespace JUSToolkit.Converters.Bin.SimpleBin
{
    public class SimpleBin2Po : IConverter<Formats.Bin.SimpleBin, Po>
    {
        public Po Convert(Formats.Bin.SimpleBin source)
        {
            var po = new Po {
                Header = new PoHeader("Jump Ultimate Stars", "TranScene", "es") {
                    LanguageTeam = "TranScene",
                }
            };

            for(int i = 0; i < source.text.Count; i++) {
                po.Add(new PoEntry(source.text[i]) {
                    Context = $"{i}"
                });
            }

            return po;
        }
    }
}
