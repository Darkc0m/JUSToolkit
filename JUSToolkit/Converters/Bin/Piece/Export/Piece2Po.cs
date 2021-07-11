using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;
using Yarhl.Media.Text;

namespace JUSToolkit.Converters.Bin.Piece.Export
{
    public class Piece2Po : IConverter<Formats.Bin.Piece, Po>
    {
        public Po Convert(Formats.Bin.Piece source)
        {
            var po = new Po {
                Header = new PoHeader("Jump Ultimate Stars", "TranScene", "es") {
                    LanguageTeam = "TranScene",
                }
            };
            int i = 0;
            foreach (PieceData d in source.data) {
                po.Add(new PoEntry(d.title) {
                    Context = $"{i} Título",
                    ExtractedComments = $"{d.unknown}-{d.icon}"
                });
                po.Add(new PoEntry(d.author) {
                    Context = $"{i} Autor"
                });
                po.Add(new PoEntry(d.startDate) {
                    Context = $"{i} Fecha inicio"
                });
                po.Add(new PoEntry(d.endDate) {
                    Context = $"{i} Fecha fin"
                });
                po.Add(new PoEntry(d.argument) {
                    Context = $"{i} Argumento"
                });
                i++;
            }

            return po;
        }
    }
}
