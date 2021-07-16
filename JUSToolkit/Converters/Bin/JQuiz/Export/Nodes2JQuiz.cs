using JUSToolkit.Formats.Bin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;
using Yarhl.FileSystem;
using Yarhl.IO;
using Yarhl.Media.Text;

namespace JUSToolkit.Converters.Bin.JQuiz.Export
{
    public class Nodes2JQuiz : IConverter<NodeContainerFormat, BinaryFormat>
    {
       public BinaryFormat Convert(NodeContainerFormat source)
        {
            var bin = new BinaryFormat();
            var table = Table.getInstance();
            var writer = new DataWriter(bin.Stream) {
                DefaultEncoding = Encoding.GetEncoding("shift_jis")
            };

            int num_entries, offset, total_entries = 3006; //Cambiar
            string[] data;
            string sentence;
            var text = new List<string>();
            var metadata = new List<int>();
            var pointers = new List<int>();
            var textPointers = new Dictionary<string, int>();
            Po po;
            writer.WriteOfType<Int32>(total_entries);
            offset = total_entries * 10 * 0x04 + 0x04;
            
            foreach(Node n in source.Root.Children) {
                po = n.TransformWith<Binary2Po>().GetFormatAs<Po>();
                num_entries = po.Entries.Count / 9;
                for(int i = 0; i < num_entries; i++) {
                    data = po.Entries[i * 9].ExtractedComments.Split(new char[] { '-' });
                    metadata.Add(byte.Parse(data[0]));
                    metadata.Add(byte.Parse(data[1]));
                    metadata.Add(Int16.Parse(data[2]));
                    for(int j = 0; j < 9; j++) {
                        sentence = po.Entries[i * 9 + j].Text == "<!empty>" ?
                            string.Empty : po.Entries[i * 9 + j].Text; //Debug original
                        if (textPointers.ContainsKey(sentence)) {
                            pointers.Add(textPointers[sentence]);
                        }
                        else {
                            textPointers.Add(sentence, offset);
                            sentence = table.Encode(sentence);
                            text.Add(sentence);
                            pointers.Add(offset);
                            offset += writer.DefaultEncoding.GetByteCount(sentence) + 1;
                        }
                    }
                }
            }

            for(int i = 0; i < total_entries; i++) {
                writer.WriteOfType<byte>((byte)metadata[i * 3]);
                writer.WriteOfType<byte>((byte)metadata[i * 3 + 1]);
                writer.WriteOfType<Int16>((Int16)metadata[i * 3 + 2]);
                for(int j = 0; j < 9; j++) {
                    writer.WriteOfType<Int32>(pointers[i * 9 + j] - (int)writer.Stream.Position);
                }
            }

            foreach(string s in text) {
                writer.Write(s);
            }

            return bin;
        }
    }
}
