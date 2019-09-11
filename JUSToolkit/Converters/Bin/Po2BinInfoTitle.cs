﻿using JUSToolkit.Formats;
using Yarhl.FileFormat;
using Yarhl.Media.Text;

namespace JUSToolkit.Converters.Bin
{
    class Po2BinInfoTitle : IConverter<Po, BinInfoTitle>
    {
        public BinInfoTitle Convert(Po source)
        {
            var bin = new BinInfoTitle();

            //Escribir en la lista cada una de las entradas del Po
            foreach(PoEntry entry in source.Entries)
            {
                bin.Text.Add(entry.Text);
            }

            return bin;
        }
    }
}
