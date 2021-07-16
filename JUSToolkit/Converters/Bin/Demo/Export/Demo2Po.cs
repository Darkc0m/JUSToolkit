using JUSToolkit.Formats.Bin;
using Yarhl.FileFormat;
using Yarhl.Media.Text;

namespace JUSToolkit.Converters.Bin.Demo.Export
{
    public class Demo2Po : IConverter<Formats.Bin.Demo, Po>
    {
        public Po Convert(Formats.Bin.Demo source)
        {
            var po = new Po {
                Header = new PoHeader("Jump Ultimate Stars", "TranScene", "es") {
                    LanguageTeam = "TranScene",
                }
            };
            int i = 0;
            foreach(DemoData v in source.data) {
                po.Add(new PoEntry(v.title) {
                    Context = $"Video {i} Titulo",
                    ExtractedComments = $"{v.id}-{v.icon}"
                });
                for(int j = 0; j < v.description.Length; j++) {
                    po.Add(new PoEntry(v.description[j]) {
                        Context = $"Video {i} Descripción {j}"
                    });
                }
            }

            return po;
        }
    }
}
