using JUSToolkit.Formats.Bin;
using System.Text;
using Yarhl.IO;

namespace JUSToolkit.Converters.Bin
{
    public class JusBinConverter
    {
        public JusFormat jus;
        public DataType type;

        protected JusFormat transform(BinaryFormat source, bool hasCount)
        {
            jus = DataFactory.createFormat(type);
            var reader = new DataReader(source.Stream) {
                DefaultEncoding = Encoding.GetEncoding("shift_jis")
            };
            JusData.reader = reader;
            reader.Stream.Position = type == DataType.Commwin ? 0x04 : 0x00;

            if (hasCount) {
                int count = reader.ReadInt32();
                for (int i = 0; i < count; i++) {
                    jus.data.Add(DataFactory.readData(type));
                }
            }
            else {
                int textStart = reader.ReadInt32()-1;
                reader.Stream.Position = 0x00;
                while (reader.Stream.Position < textStart) {
                    jus.data.Add(DataFactory.readData(type));
                }
            }          

            return jus;
        }
    }
}
