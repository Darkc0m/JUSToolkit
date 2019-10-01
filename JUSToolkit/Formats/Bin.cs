namespace JUSToolkit.Formats
{
    using Yarhl.FileFormat;
    using System.Collections.Generic;
    public class Bin : Format
    {
        public List<string> Text;
        public List<int> Pointers;

        public Bin(){
            Text = new List<string>();
            Pointers = new List<int>();
        }
    }
}
