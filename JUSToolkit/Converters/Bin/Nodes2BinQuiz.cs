using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;
using Yarhl.FileSystem;
using Yarhl.IO;
using Yarhl.Media.Text;
using JUSToolkit.Formats;

namespace JUSToolkit.Converters.Bin
{
    class Nodes2BinQuiz : IConverter<NodeContainerFormat, BinQuiz>
    {
        public DataReader OriginalFile { get; set; }

        public BinQuiz Convert(NodeContainerFormat container)
        {
            var bin = new BinQuiz();
            string sentence;
            Dictionary<int, int> transformOffset = new Dictionary<int, int>();

            //Sacar todo el texto de los archivos
            foreach(Node n in container.Root.Children)
            {
                foreach(PoEntry entry in n.Transform<Po2Binary, BinaryFormat, Po>().GetFormatAs<Po>().Entries)
                {
                    sentence = entry.Text;
                    if (sentence.Equals("<!empty>"))
                        sentence = string.Empty;

                    bin.Text.Add(sentence);
                }
            }


            int textOffset;
            int offset;
            int updatedOffset;

            OriginalFile.Stream.Position = 0x08;
            textOffset = OriginalFile.ReadInt32() + 8;
            OriginalFile.Stream.Position = textOffset;

            updatedOffset = textOffset;

            foreach(string entry in bin.Text)
            {
                offset = (int)OriginalFile.Stream.Position;
                transformOffset.Add(offset, updatedOffset);

                updatedOffset += OriginalFile.DefaultEncoding.GetByteCount(entry) + 1;

                OriginalFile.ReadString();
            }

            OriginalFile.Stream.Position = 0x00;

            int pointerValue;

            for(int i = 0; i < textOffset / 4; i++)
            {
                pointerValue = OriginalFile.ReadInt32();

                offset = (int)OriginalFile.Stream.Position - 4 + pointerValue;

                if (transformOffset.ContainsKey(offset))
                {
                    offset = transformOffset[offset];
                    pointerValue = offset - i * 4;
                }

                bin.Pointers.Add(pointerValue);
            }

            return bin;
        }
    }
}
