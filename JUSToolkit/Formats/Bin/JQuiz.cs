using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;

namespace JUSToolkit.Formats.Bin
{
    class JQuiz : IFormat
    {
    }

    public class JquizData : JusData
    {
        public int mangaCode, unknown, num;
        public string photo, manga, question;
        public string[] answers;

        public JquizData()
        {
            answers = new string[4];
        }

        public static JquizData readData()
        {
            if (reader == null) {
                return null;
            }
            var mangaIndex = MangaIndex.getInstance();

            var data = new JquizData();
            data.mangaCode = reader.ReadByte();
            data.manga = mangaIndex.getMangaName(data.mangaCode);
            data.unknown = reader.ReadByte();
            data.num = reader.ReadInt16();
            data.photo = readPointerText();
            data.question = readMultipleEntry(4);
            for (int i = 0; i < data.answers.Length; i++) {
                data.answers[i] = readPointerText();
            }

            return data;
        }
    }
}
