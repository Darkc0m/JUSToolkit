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
    class Binary2BinDeckP : IConverter<BinaryFormat, BinDeckP>
    {
        public BinDeckP Convert(BinaryFormat source)
        {
            DataReader reader = new DataReader(source.Stream);

            var bin = new BinDeckP();
            string sentence;

            reader.Stream.Position = 0x00;

            sentence = ReadStringUntill(reader, 0x13, Encoding.GetEncoding(1200));
            bin.Text.Add(sentence);
            reader.Stream.Position = 0x14;

            sentence = ReadStringUntill(reader, 0x33, Encoding.GetEncoding(932));
            bin.Text.Add(sentence);

            return bin;
        }

        public string ReadStringUntill(DataReader reader, int offset, Encoding encoding)
        {
            List<char> list = new List<char>();
            bool zero = false;
            char c;
            
            while (reader.Stream.Position <= offset && !zero)
            {
                c = reader.ReadChar(encoding);
                if (c == '\0')
                {
                    zero = true;
                }
                else
                {
                    list.Add(c);
                }
            }

            return new string(list.ToArray());
        }
    }
}
