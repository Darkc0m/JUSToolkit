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
    class Binary2BinGBattleMission : IConverter<BinaryFormat, BinGBattleMission>
    {
        public BinGBattleMission Convert(BinaryFormat source)
        {
            DataReader reader = new DataReader(source.Stream)
            {
                DefaultEncoding = Encoding.GetEncoding(932)
            };

            var bin = new BinGBattleMission();

            string sentence;

            reader.Stream.Position = 0x00;

            int numEntries = reader.ReadInt32();
            int offset = (int)reader.Stream.Position;

            for(int i = 0; i < numEntries; i++)
            {
                reader.Stream.Position = offset;                
                sentence = reader.ReadString();
                bin.Text.Add(sentence);
                offset += BinGBattleMission.blockSize;
            }

            return bin;
        }
    }
}
