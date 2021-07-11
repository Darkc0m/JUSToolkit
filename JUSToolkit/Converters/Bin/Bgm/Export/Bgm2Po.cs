using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;
using Yarhl.Media.Text;

namespace JUSToolkit.Converters.Bin.Bgm.Export
{
    public class Bgm2Po : IConverter<Formats.Bin.Bgm, Po>
    {
        public Po Convert(Formats.Bin.Bgm source)
        {
            var po = new Po {
                Header = new PoHeader("Jump Ultimate Stars", "TranScene", "es") {
                    LanguageTeam = "TranScene",
                }
            };
            int i = 0;
            foreach (BgmData s in source.data) {
                po.Add(new PoEntry(s.title) {
                    Context = $"Canción {i} Titulo",
                    ExtractedComments = $"{s.unk1}-{s.unk2}-{s.icon}"
                });
                po.Add(new PoEntry(s.description) {
                    Context = $"Canción {i} Descripción"
                });
                i++;
            }

            return po;
        }
    }
}
