using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;
using Yarhl.Media.Text;

namespace JUSToolkit.Converters.Bin.Location.Export
{
    public class Location2Po : IConverter<Formats.Bin.Location, Po>
    {
        public Po Convert(Formats.Bin.Location source)
        {
            var po = new Po {
                Header = new PoHeader("Jump Ultimate Stars", "TranScene", "es") {
                    LanguageTeam = "TranScene",
                }
            };
            int i = 0;
            foreach (LocationData p in source.places) {
                po.Add(new PoEntry(p.name) {
                    Context = $"Lugar {i}",
                    ExtractedComments = $"{p.unk1}-{p.unk2}"
                });
            }

            return po;
        }
    }
}
