using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace JUSToolkit.Converters.Bin.JQuiz
{
    public class JquizData : JusData
    {
        public int mangaCode, unknown, num;
        public string photo, manga;
        public string[] question;
        public string[] answers;

        public JquizData()
        {
            question = new string[4];
            answers = new string[4];
        }

        public static JquizData readData()
        {
            if(reader == null) {
                return null;
            }
            var data = new JquizData();
            data.mangaCode = reader.ReadByte();
            //data.manga = Utils.getManga(data.mangaCode);
            data.unknown = reader.ReadByte();
            data.num = reader.ReadInt16();
            data.photo = readPointerText();
            for (int i = 0; i < data.question.Length; i++) {
                data.question[i] = readPointerText();
            }
            for (int i = 0; i < data.answers.Length; i++) {
                data.answers[i] = readPointerText();
            }

            return data;
        }
    }
}
