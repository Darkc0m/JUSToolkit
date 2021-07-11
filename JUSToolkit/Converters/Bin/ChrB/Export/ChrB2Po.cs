using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;
using Yarhl.Media.Text;

namespace JUSToolkit.Converters.Bin.ChrB.Export
{
    public class ChrB2Po : IConverter<Formats.Bin.ChrB, Po>
    {
        public Po Convert(Formats.Bin.ChrB source)
        {
            var po = new Po {
                Header = new PoHeader("Jump Ultimate Stars", "TranScene", "es") {
                    LanguageTeam = "TranScene",
                }
            };

            int i = 0;
            foreach(ChrBData data in source.data) {
                foreach(string text in data.text) {
                    po.Add(new PoEntry(text) {
                        Context = $"{i}"
                    });
                    i++;
                }
            }

            return po;
        }
    }
}
