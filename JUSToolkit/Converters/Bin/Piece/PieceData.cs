using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace JUSToolkit.Converters.Bin.Piece
{
    public class PieceData : JusData
    {
        public string title, startDate, endDate, argument, author;
        public int unknown, icon;

        public PieceData()
        {
            argument = string.Empty;
            author = string.Empty;
            title = readPointerText();
            for (int i = 0; i < 2; i++) {
                author += $"{readPointerText(false)}\n";
            }
            startDate = readPointerText();
            endDate = readPointerText();
            for (int i = 0; i < 18; i++) {
                argument += $"{readPointerText(false)}\n";
            }
            unknown = reader.ReadInt16();
            icon = reader.ReadInt16();
        }
    }
}
