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
    class BinGBattleMission2Po : IConverter<BinGBattleMission, Po>
    {
        public Po Convert(BinGBattleMission source)
        {
            Po poExport = new Po
            {
                Header = new PoHeader("Jump Ultimate Stars", "TranScene", "es")
                {
                    LanguageTeam = "TranScene",
                }
            };
            int i = 0;
            foreach(string sentence in source.Text)
            {
                poExport.Add(new PoEntry(sentence) { Context = i.ToString() });
                i++;
            }

            return poExport;
        }
    }
}
