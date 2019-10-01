using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;
using Yarhl.IO;
using JUSToolkit.Formats;

namespace JUSToolkit.Converters.Bin
{
    class BinQuiz2Binary : IConverter<BinQuiz, BinaryFormat>
    {
        public BinaryFormat Convert(BinQuiz source)
        {
            var bin = new BinaryFormat();

            DataWriter writer = new DataWriter(bin.Stream)
            {
                DefaultEncoding = Encoding.GetEncoding(932)
            };

            foreach(int i in source.Pointers)
            {
                writer.WriteOfType<Int32>(i);
            }

            foreach(string sentence in source.Text)
            {
                writer.Write(sentence);
            }

            return bin;
        }
    }
}
