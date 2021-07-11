using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JUSToolkit.Converters.Bin.Info
{
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
