using JUSToolkit.Formats.Bin;
using Yarhl.FileFormat;
using Yarhl.Media.Text;

namespace JUSToolkit.Converters.Bin.Rulemess.Export
{
    public class Rulemess2Po : IConverter<Formats.Bin.Rulemess, Po>
    {
        public Po Convert(Formats.Bin.Rulemess source)
        {
            var po = new Po {
                Header = new PoHeader("Jump Ultimate Stars", "TranScene", "es") {
                    LanguageTeam = "TranScene",
                }
            };

            int i = 0;
            foreach (RulemessData data in source.data) {
                po.Add(new PoEntry(data.text) {
                    Context = $"{i}"
                });
                i++;
            }

            return po;
        }
    }
}
