using System;
using System.Text;
using Yarhl.FileFormat;
using Yarhl.IO;

namespace JUSToolkit.Converters.Bin
{
    using JUSToolkit.Formats;

    class Bin2Binary : IConverter<Bin, BinaryFormat>
    {
        public BinaryFormat Convert(Bin source)
        {
            var bin = new BinaryFormat();

            DataWriter writer = new DataWriter(bin.Stream)
            {
                DefaultEncoding = Encoding.GetEncoding(932)
            };

            foreach(int pointer in source.Pointers)
            {
                writer.WriteOfType<Int16>((Int16)pointer);
            }
            foreach(string text in source.Text)
            {
                writer.Write(text);
            }

            return bin;
        }
    }
}
