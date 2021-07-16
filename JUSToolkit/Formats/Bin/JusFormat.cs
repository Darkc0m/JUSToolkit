using System.Collections.Generic;
using Yarhl.FileFormat;
using Yarhl.IO;

namespace JUSToolkit.Formats.Bin
{
    public class JusFormat : IFormat
    {
        public List<JusData> data;

        public JusFormat()
        {
            data = new List<JusData>();
        }
    }

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

        protected static string readMultipleEntry(int lines)
        {
            string text = "";
            for (int i = 0; i < lines; i++) {
                text += $"{readPointerText(false)}\n";
            }

            return text.Trim();
        }
    }
}
