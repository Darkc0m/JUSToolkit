using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;

namespace JUSToolkit
{
    class MangaIndex
    {
        string indexPath = "../../Resources/JquizMangas.txt";
        Dictionary<int, string> indexDic;
        static MangaIndex instance;

        private MangaIndex()
        {
            indexDic = new Dictionary<int, string>();
            readTable();
        }

        public static MangaIndex getInstance()
        {
            if (instance == null) {
                instance = new MangaIndex();
            }

            return instance;
        }

        public string getMangaName(int index)
        {
            return indexDic[index];
        }

        private void readTable()
        {
            string line;
            string[] chars;
            var reader = new TextReader(DataStreamFactory.FromFile(indexPath, FileOpenMode.Read));

            while (!reader.Stream.EndOfStream) {
                line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                if (line[0] == '#')
                    continue;

                chars = line.Split('=');
                indexDic.Add(Convert.ToInt32(chars[0], 16), chars[1]);
            }
        }
    }
}
