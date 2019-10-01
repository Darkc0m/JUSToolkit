using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;
using Yarhl.IO;
using Yarhl.Media.Text;
using JUSToolkit.Formats;

namespace JUSToolkit.Converters.Bin
{
    class Po2BinGGalaxy : IConverter<Po, BinGGalaxy>
    {
        public DataReader OriginalFile { get; set; }

        public BinGGalaxy Convert(Po source)
        {
            var bin = new BinGGalaxy();
            string sentence;
            int j = 0;
            List<byte[]> vs = new List<byte[]>();

            bin.NumBlocks = GetBlocks(OriginalFile);
            bin.BlockStart = GetBlockStart(OriginalFile);

            for (int i = 0; i < bin.NumBlocks[0]; i++)
            {
                sentence = source.Entries[j].Text;
                j++;
                if (sentence == "<!empty>")
                    sentence = string.Empty;

                bin.Text.Add(sentence);

                OriginalFile.Stream.Position += 40;

                vs.Add(OriginalFile.ReadBytes(BinGGalaxy.blockSize[0] - 40));

            }

            bin.code.Add(vs);

            vs = new List<byte[]>();

            for (int i = 0; i < bin.NumBlocks[1]; i++)
            {
                sentence = source.Entries[j].Text;
                j++;
                if (sentence == "<!empty>")
                    sentence = string.Empty;

                bin.Text.Add(sentence);

                OriginalFile.Stream.Position += 40;
                vs.Add(OriginalFile.ReadBytes(BinGGalaxy.blockSize[1] - 40));
            }

            bin.code.Add(vs);

            vs = new List<byte[]>();

            for (int i = 0; i < bin.NumBlocks[3]; i++)
            {
                vs.Add(OriginalFile.ReadBytes(BinGGalaxy.blockSize[3]));
            }

            bin.code.Add(vs);

            vs = new List<byte[]>();

            for (int i = 0; i < bin.NumBlocks[2]; i++)
            {
                sentence = source.Entries[j].Text;
                j++;
                if (sentence == "<!empty>")
                    sentence = string.Empty;

                bin.Text.Add(sentence);

                OriginalFile.Stream.Position += 40;
                vs.Add(OriginalFile.ReadBytes(BinGGalaxy.blockSize[2] - 40));
            }

            bin.code.Add(vs);

            return bin;
        }


        public List<int> GetBlocks(DataReader reader)
        {
            List<int> NumBlocks = new List<int>();
            OriginalFile.Stream.Position = 0x00;

            for(int i = 0; i < 4; i++)
                NumBlocks.Add(OriginalFile.ReadInt16());

            return NumBlocks;
        }

        public List<int> GetBlockStart(DataReader reader)
        {
            List<int> BlockStart = new List<int>();

            for (int i = 0; i < 4; i++)
                BlockStart.Add(OriginalFile.ReadInt32());

            return BlockStart;
        }
    }
}
