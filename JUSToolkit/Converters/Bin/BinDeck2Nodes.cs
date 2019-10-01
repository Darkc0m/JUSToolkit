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
    class BinDeck2Nodes : IConverter<NodeContainerFormat, BinDeck>
    {
        public BinDeck Convert(NodeContainerFormat container)
        {
            var bin = new BinDeck();
            string sentence;

            foreach(Node n in container.Root.Children)
            {
                sentence = n.Transform<Binary2BinDeck, BinaryFormat, BinDeck>().GetFormatAs<BinDeck>().Text[0];

                bin.Text.Add(sentence);
                bin.Files.Add(n.Name);

            }

            return bin;
        }
    }
}
