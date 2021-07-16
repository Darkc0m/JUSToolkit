using JUSToolkit.Formats.Bin;
using System;
using System.Collections.Generic;
using System.Text;
using Yarhl.FileFormat;
using Yarhl.IO;

namespace JUSToolkit.Converters.Bin.Deck.Import
{
    public class Deck2Binary : IConverter<Formats.Bin.Deck, BinaryFormat>
    {
        public BinaryFormat Convert(Formats.Bin.Deck source)
        {
            var bin = new BinaryFormat();
            var writer = new DataWriter(bin.Stream) {
                DefaultEncoding = Encoding.GetEncoding("shift_jis")
            };
            var table = Table.getInstance();
            var text = new List<string>();
            var pointers = new List<int>();
            string[] entryText;
            int offset = source.data.Count * 10 * 0x04 + 1;
            var textPointers = new Dictionary<string, int>() { { string.Empty, offset - 1} };
            text.Add(string.Empty);

            foreach(DeckData data in source.data) {
                entryText = data.text.Split(new[] { "\n" }, 9, StringSplitOptions.RemoveEmptyEntries);
                for(int i = 0; i < 9; i++) {
                    if(i < entryText.Length) {
                        if (textPointers.ContainsKey(entryText[i])) {
                            pointers.Add(textPointers[entryText[i]]);
                        }
                        else {
                            textPointers.Add(entryText[i], offset);
                            string encodedSentence = table.Encode(entryText[i]);
                            text.Add(encodedSentence);
                            pointers.Add(offset);
                            offset += writer.DefaultEncoding.GetByteCount(encodedSentence) + 1;
                        }
                    }
                    else {
                        pointers.Add(textPointers[string.Empty]);
                    }
                }
                pointers.Add(textPointers[string.Empty]);
            }

            for(int i = 0; i < pointers.Count; i++) {
                writer.WriteOfType<Int32>(pointers[i] - i * 4);
            }
            foreach(string s in text) {
                writer.Write(s);
            }

            return bin;
        }
    }
}
