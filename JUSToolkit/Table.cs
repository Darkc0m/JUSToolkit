using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Yarhl.IO;
using Yarhl.Media.Text;

namespace JUSToolkit
{
    class Table
    {
        string tablePath = "C:/Users/msi/source/repos/JUSToolkit/JUSToolkit/Resources/table.tbl";//"../../Resources/table.tbl";
        Replacer replacer;
        static Table instance;

        private Table()
        {
            replacer = new Replacer();
            readTable();
        }

        public static Table getInstance()
        {
            if(instance == null) {
                instance = new Table();
            }

            return instance;
        } 

        public string Encode(string s)
        {
            return replacer.TransformBackward(s);
        }

        private void readTable()
        {
            string line;
            string[] chars;
            var reader = new TextReader(DataStreamFactory.FromFile(tablePath, FileOpenMode.Read));

            while (!reader.Stream.EndOfStream) {
                line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                if (line[0] == '/')
                    continue;

                chars = line.Split('=');
                replacer.Add(chars[1], chars[0]);
            }
        }
    }
}
