﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;
using JUSToolkit.Formats;
using Yarhl.IO;

namespace JUSToolkit.Converters.Bin
{
    class Binary2BinDeck : IConverter<BinaryFormat, BinDeck>
    {
        public BinDeck Convert(BinaryFormat source)
        {
            DataReader reader = new DataReader(source.Stream)
            {
                DefaultEncoding = Encoding.GetEncoding(932)
            };

            reader.Stream.Position = 0x40;

            var bin = new BinDeck()
            {
                text = ReadStringUntill(reader, 0x5B, Encoding.GetEncoding(932))
            };

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
