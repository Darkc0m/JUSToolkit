﻿using System;
using Yarhl.FileFormat;
using JUSToolkit.Formats;
using Yarhl.IO;
using System.Text;

namespace JUSToolkit.Converters.Bin
{
    using JUSToolkit.Formats;
    class Binary2Bin : IConverter<BinaryFormat, Bin>
    {
        public Bin Convert(BinaryFormat source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            DataReader reader = new DataReader(source.Stream)
            {
                DefaultEncoding = Encoding.GetEncoding(932)
            };
            string sentence;

            var bin = new Bin();

            Go2Text(reader);

            while (!reader.Stream.EndOfStream)
            {
                sentence = reader.ReadString();
                if (string.IsNullOrEmpty(sentence))
                {
                    sentence = "<!empty>";
                }
                bin.Text.Add(sentence);
            }

            return bin;
        }

        public void Go2Text(DataReader reader)
        {
            reader.Stream.Position = 0x00;
            int PointerValue = reader.ReadInt16();

            switch (PointerValue)
            {
                case 0x0029:
                case 0x005B:
                case 0x0034:
                case 0x0032:
                case 0x0D04:
                case 0x0059:
                    reader.Stream.Position += 2;
                    PointerValue = reader.ReadInt16();
                    break;
                case 0x0BBE:
                    reader.Stream.Position = 0x08;
                    PointerValue = reader.ReadInt32();
                    reader.Stream.Position -= 2;
                    break;
                default:
                    break;
            }

            reader.Stream.Position += PointerValue - 2;

        }
    }
}
