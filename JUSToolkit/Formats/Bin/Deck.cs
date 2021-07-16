using JUSToolkit.Converters.Bin.Deck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;

namespace JUSToolkit.Formats.Bin
{
    public class Deck : JusFormat
    {

    }

    public class DeckData : JusData
    {
        public string text;

        public DeckData()
        {
            text = string.Empty;
            for (int i = 0; i < 9; i++) {
                text += $"{readPointerText(false)}\n";
            }

            reader.Stream.Position += 0x04;
        }

        public DeckData(string text)
        {
            this.text = text;
        }
    }
}
