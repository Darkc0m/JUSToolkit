using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;
using Yarhl.IO;
using Yarhl.Media.Text;
using JUSToolkit.Formats;

namespace JUSToolkit.Converters.Bin
{
    class Po2BinGBattleMission : IConverter<Po, BinGBattleMission>
    {
        public DataReader OriginalFile { get; set; }

        public BinGBattleMission Convert(Po source)
        {
            var bin = new BinGBattleMission();
            string sentence;

            OriginalFile.Stream.Position = 0x04;

            foreach(PoEntry entry in source.Entries)
            {
                sentence = entry.Text;
                if (sentence == "<!empty>")
                    sentence = string.Empty;

                bin.Text.Add(sentence);

                OriginalFile.Stream.Position += 40;
                bin.code.Add(OriginalFile.ReadBytes(BinGBattleMission.blockSize - 40));

            }

            return bin;
        }
    }
}
