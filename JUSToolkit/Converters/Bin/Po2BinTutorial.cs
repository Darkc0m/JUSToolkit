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
    class Po2BinTutorial : IConverter<Po, BinTutorial>
    {
        public DataReader OriginalFile { get; set; }

        public BinTutorial Convert(Po source)
        {   
            Encoding SJIS = Encoding.GetEncoding(932);

            OriginalFile.Stream.Position = 0x00;
            int numPointers = OriginalFile.ReadInt32() / 2;

            var bin = new BinTutorial();

            int oldOffset = 0, updatedOffset = 0;
            string sentence;
            Dictionary<int, int> offsets = new Dictionary<int, int>();

            foreach(PoEntry entry in source.Entries)
            {
                oldOffset += SJIS.GetByteCount(entry.Original) + 1;
                sentence = entry.Text;    //Esto es porque el texto del po no es el mismo que el que se va a meter en el juego asi que puede no medir lo mismo volver despues
                updatedOffset += SJIS.GetByteCount(sentence) + 1;
                offsets.Add(oldOffset, updatedOffset);
                bin.Text.Add(sentence);
            }

            int pointer;
            OriginalFile.Stream.Position = 0x00;
            for(int i = 0; i < numPointers; i++)
            {
                pointer = OriginalFile.ReadInt16();

                if (offsets.ContainsKey(pointer))
                    pointer = offsets[pointer];

                bin.Pointers.Add(pointer);
            }

            return bin;
        }
    }
}
