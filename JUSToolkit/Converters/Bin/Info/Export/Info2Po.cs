using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;
using Yarhl.Media.Text;

namespace JUSToolkit.Converters.Bin.Info.Export
{
    public class Info2Po : IConverter<Formats.Bin.Info, Po>
    {
        public Po Convert(Formats.Bin.Info source)
        {
            var po = new Po {
                Header = new PoHeader("Jump Ultimate Stars", "TranScene", "es") {
                    LanguageTeam = "TranScene",
                }
            };

            for (int i = 0; i < source.data.Count; i++) {
                po.Add(new PoEntry(((InfoData)source.data[i]).text) {
                    Context = $"{i}"
                });
            }

            return po;
        }
    }
}
