using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace JUSToolkit.Converters.Bin
{
    public abstract class JusData
    {
        public static DataReader reader;
        protected static string readPointerText(bool emptyCheck = true)
        {
            reader.Stream.PushToPosition(reader.Stream.Position + reader.ReadInt32());
            string text = reader.ReadString();
            reader.Stream.PopPosition();
            if (emptyCheck) {
                return string.IsNullOrEmpty(text) ? "<!empty>" : text;
            }
            else {
                return text;
            }
        }
    }
}
