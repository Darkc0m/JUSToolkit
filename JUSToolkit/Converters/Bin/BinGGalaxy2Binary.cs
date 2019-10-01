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
    class BinGGalaxy2Binary : IConverter<BinGGalaxy, BinaryFormat>
    {
        public BinaryFormat Convert(BinGGalaxy source)
        {
            var bin = new BinaryFormat();

            DataWriter writer = new DataWriter(bin.Stream)
            {
                DefaultEncoding = Encoding.GetEncoding(932)
            };

            string sentence;
            int j = 0;
            int offset = source.BlockStart[0] + 40;

            foreach(int i in source.NumBlocks)
                writer.WriteOfType<Int16>((Int16)i);

            foreach (int i in source.BlockStart)
                writer.WriteOfType<Int32>(i);

            for(int i = 0; i < source.NumBlocks[0]; i++)
            {
                sentence = source.Text[j];
                j++;

                writer.Write(sentence);

                while (writer.Stream.Position < offset)
                    writer.WriteOfType<byte>(0x00);

                offset += BinGGalaxy.blockSize[0];
                writer.Write(source.code[0][i]);
            }

            offset = source.BlockStart[1] + 40;

            for (int i = 0; i < source.NumBlocks[1]; i++)
            {
                sentence = source.Text[j];
                j++;

                writer.Write(sentence);

                while (writer.Stream.Position < offset)
                    writer.WriteOfType<byte>(0x00);

                offset += BinGGalaxy.blockSize[1];
                writer.Write(source.code[1][i]);
            }

            for (int i = 0; i < source.NumBlocks[3]; i++)
            {
                writer.Write(source.code[2][i]);
            }

            offset = source.BlockStart[2] + 40;

            for (int i = 0; i < source.NumBlocks[2]; i++)
            {
                sentence = source.Text[j];
                j++;

                writer.Write(sentence);

                while (writer.Stream.Position < offset)
                    writer.WriteOfType<byte>(0x00);

                offset += BinGGalaxy.blockSize[2];
                writer.Write(source.code[3][i]);
            }

            return bin;
        }
    }
}
