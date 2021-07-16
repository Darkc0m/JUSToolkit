using JUSToolkit.Converters.Bin.Piece;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;

namespace JUSToolkit.Formats.Bin
{
    public class Piece : JusFormat
    {

    }

    public class PieceData : JusData
    {
        public string title, startDate, endDate, argument, author;
        public int unknown, icon;

        public PieceData()
        {
            title = readPointerText();
            author = readMultipleEntry(2);
            startDate = readPointerText();
            endDate = readPointerText();
            argument = readMultipleEntry(18);
            unknown = reader.ReadInt16();
            icon = reader.ReadInt16();
        }
    }
}
