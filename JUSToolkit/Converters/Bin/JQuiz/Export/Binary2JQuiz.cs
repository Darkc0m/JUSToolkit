using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                DefaultEncoding = Encoding.GetEncoding("shift_jis")
            };
            JusData.reader = reader;
            BinaryFormat poBin;
            int numQuestions = reader.ReadInt32();

            string currentManga = "Dr. Slump";
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
                po.Add(new PoEntry(q.photo) {
                    Context = $"Pregunta {i} foto",
                    ExtractedComments = $"{q.mangaCode}-{q.unknown}-{q.num}"
                });
                for (int j = 0; j < q.question.Length; j++) {
                    po.Add(new PoEntry(q.question[j]) {
                        Context = $"Pregunta {i} enunciado {j}"
                    });
                }
                for (int j = 0; j < q.answers.Length; j++) {
                    po.Add(new PoEntry(q.answers[j]) {
                        Context = $"Pregunta {i} respuesta {j}"
                    });
                }
            }

            poBin = ConvertFormat.With<Po2Binary>(po) as BinaryFormat;
            container.Root.Add(new Node($"jquiz-{mangaCount}-{currentManga}.po", poBin));

            return container;
        }
    }
}
