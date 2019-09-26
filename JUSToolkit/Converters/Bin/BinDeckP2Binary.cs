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
    class BinDeckP2Binary : IConverter<BinDeckP, BinaryFormat>
    {
        public BinaryFormat Convert(BinDeckP source)
        {
            var bin = new BinaryFormat();
            string sentence;

            DataWriter writer = new DataWriter(bin.Stream);
            //Escribo el primer texto
            sentence = source.Text.ElementAt(0);
            writer.Write(sentence, false, Encoding.GetEncoding(1200));
            while(writer.Stream.Position < 0x14)
                writer.WriteOfType<byte>(0x00);
            //Escribo el segundo texto
            sentence = source.Text.ElementAt(1);
            writer.Write(sentence, false, Encoding.GetEncoding(932));
            while (writer.Stream.Position < 0x34)
                writer.WriteOfType<byte>(0x00);
            //Escribo el código
            writer.Write(source.code);

            return bin;
        }
    }
}
