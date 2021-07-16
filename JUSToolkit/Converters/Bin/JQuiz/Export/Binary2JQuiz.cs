using JUSToolkit.Formats.Bin;
using System.Text;
using Yarhl.FileFormat;
using Yarhl.FileSystem;
using Yarhl.IO;
using Yarhl.Media.Text;

namespace JUSToolkit.Converters.Bin.JQuiz
{
    public class Binary2JQuiz : IConverter<BinaryFormat, NodeContainerFormat>
    {
        public NodeContainerFormat Convert(BinaryFormat source)
        {
            var container = new NodeContainerFormat();

            var po = new Po {
                Header = new PoHeader("Jump Ultimate Stars", "TranScene", "es") {
                    LanguageTeam = "TranScene",
                }
            };

            var reader = new DataReader(source.Stream) {
                DefaultEncoding = Encoding.GetEncoding("shift_jis"),
            };
            JusData.reader = reader;
            reader.Stream.Position = 0x00;
            BinaryFormat poBin;
            var mangaIndex = MangaIndex.getInstance();
            int numQuestions = reader.ReadInt32();

            string currentManga = mangaIndex.getMangaName(0xFF);
            int mangaCount = 0;

            for (int i = 0; i < numQuestions; i++) {
                var q = JquizData.readData();
                if (q.manga != currentManga) {
                    poBin = ConvertFormat.With<Po2Binary>(po) as BinaryFormat;
                    container.Root.Add(new Node($"jquiz-{mangaCount}-{currentManga}.po", poBin));
                    po = new Po {
                        Header = new PoHeader("Jump Ultimate Stars", "TranScene", "es") {
                            LanguageTeam = "TranScene",
                        }
                    };
                    mangaCount++;
                    currentManga = q.manga;
                }
                po.Add(new PoEntry(q.question) { 
                    Context = $"{i} - Question",
                    ExtractedComments = $"{q.photo}-{q.mangaCode}-{q.unknown}-{q.num}"
                });
                for (int j = 0; j < q.answers.Length; j++) {
                    po.Add(new PoEntry(q.answers[j]) {
                        Context = $"{i} - Answer {j}"
                    });
                }
            }

            poBin = ConvertFormat.With<Po2Binary>(po) as BinaryFormat;
            container.Root.Add(new Node($"jquiz-{mangaCount}-{currentManga}.po", poBin));

            return container;
        }
    }
}
