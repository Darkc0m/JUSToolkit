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
    class BinGBattleMission2Binary : IConverter<BinGBattleMission, BinaryFormat>
    {
        public BinaryFormat Convert(BinGBattleMission source)
        {
            var bin = new BinaryFormat();

            DataWriter writer = new DataWriter(bin.Stream)
            {
                DefaultEncoding = Encoding.GetEncoding(932)
            };

            int i = 0;
            int offset = 0x2C;

            writer.WriteOfType<Int32>(source.Text.Count);

            foreach(string sentence in source.Text)
            {
                writer.Write(sentence);

                while (writer.Stream.Position < offset)
                    writer.WriteOfType<byte>(0x00);

                offset += BinGBattleMission.blockSize;

                writer.Write(source.code[i]);
                i++;

            }

            return bin;
        }
    }
}
