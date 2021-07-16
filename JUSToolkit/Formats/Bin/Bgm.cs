namespace JUSToolkit.Formats.Bin
{
    public class Bgm : JusFormat
    {

    }

    public class BgmData : JusData
    {
        public string title;
        public string description;
        public int unk1, unk2, icon;

        public BgmData()
        {
            description = string.Empty;
            title = readPointerText();
            for (int i = 0; i < 3; i++) {
                description += $"{readPointerText(false)}\n";
            }
            unk1 = reader.ReadInt16();
            unk2 = reader.ReadInt16();
            icon = reader.ReadInt32();
        }
    }
}
