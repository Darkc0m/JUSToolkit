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
    class Binary2BinGGalaxy : IConverter<BinaryFormat, BinGGalaxy>
    {
        public BinGGalaxy Convert(BinaryFormat source)
        {
            DataReader reader = new DataReader(source.Stream)
            {
                DefaultEncoding = Encoding.GetEncoding(932)
            };

            var bin = new BinGGalaxy();

            reader.Stream.Position = 0x00;

            int[] numEntries = getNumEntries(reader);
            int[] position = getPositions(reader);

            int offset;
            string sentence;

            for(int i = 0; i < 3; i++)
            {
                reader.Stream.Position = position[i];
                offset = position[i];
                for (int j = 0; j < numEntries[i]; j++)
                {
                    reader.Stream.Position = offset;
                    sentence = reader.ReadString();
                    bin.Text.Add(sentence);
                    offset += BinGGalaxy.blockSize[i];
                }
            }

            return bin;
        }

        public int[] getNumEntries(DataReader reader)
        {
            return new int[] { reader.ReadInt16(), reader.ReadInt16(), reader.ReadInt16(), reader.ReadInt16() };
        }
        
        public int[] getPositions(DataReader reader)
        {
            return new int[] { reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32() };
        }
    }
}
