using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileFormat;
using Yarhl.FileSystem;
using JUSToolkit.Formats;

namespace JUSToolkit.Converters.Bin
{
    class BinDeckP2Nodes : IConverter<NodeContainerFormat, BinDeckP>
    {
        public BinDeckP Convert(NodeContainerFormat container)
        {
            var bin = new BinDeckP();
            List<string> Text;

            foreach(Node n in container.Root.Children)
            {
                Text = n.Transform<Binary2BinDeckP, BinaryFormat, BinDeckP>()
                    .GetFormatAs<BinDeckP>().Text;

                foreach(string sentence in Text)
                {
                    bin.Text.Add(sentence);
                    bin.Files.Add(n.Name);
                }

            }

            return bin;
        }
    }
}
