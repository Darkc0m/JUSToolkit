using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;
using Yarhl.Media.Text;

namespace JUSToolkit.Converters.Bin.Commwin.Export
{
    public class Commwin2Po : IConverter<Formats.Bin.Commwin, Po>
    {
        public Po Convert(Formats.Bin.Commwin source)
        {
            var po = new Po {
                Header = new PoHeader("Jump Ultimate Stars", "TranScene", "es") {
                    LanguageTeam = "TranScene",
                }
            };

            int i = 0;
            foreach(CommwinData data in source.data) {
                for(int j = 0; j < data.text.Length; j++) {
                    po.Add(new PoEntry(data.text[j]) {
                        Context = $"{i}"
                    });
                    i++;
                }
            }

            return po;
        }
    }
}
