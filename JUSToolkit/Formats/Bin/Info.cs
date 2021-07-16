using JUSToolkit.Converters.Bin.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;

namespace JUSToolkit.Formats.Bin
{
    public class Info : JusFormat
    {

    }

    public class InfoData : JusData
    {
        public string text;

        public InfoData()
        {
            text = string.Empty;
            for (int i = 0; i < 3; i++) {
                text += $"{readPointerText(false)}\n";
            }
        }

        public InfoData(string text)
        {
            this.text = text;
        }
    }
}
