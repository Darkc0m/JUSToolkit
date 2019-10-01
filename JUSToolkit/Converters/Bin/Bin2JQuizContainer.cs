using System;
using System.Collections.Generic;
using JUSToolkit.Formats;
using Yarhl.FileFormat;
using Yarhl.FileSystem;
using Yarhl.Media.Text;

namespace JUSToolkit.Converters.Bin
{
    using JUSToolkit.Formats;

    public class Bin2JQuizContainer : IConverter<Bin, NodeContainerFormat>
    {

        //private readonly Dictionary<string, string> sagas;
        private readonly IList<string> sagaEnd;

        public Bin2JQuizContainer()
        {
            /*sagas = new Dictionary<string, string>();
            SetSagas();*/
            sagaEnd = new List<string>();
            SetSagaEnd();
        }

        public NodeContainerFormat Convert(Bin source)
        {

            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var container = new NodeContainerFormat();

            int i = 0, j = 0;
            Po poExport = new Po
            {
                Header = new PoHeader("Jump Ultimate Stars", "TranScene", "es")
                {
                    LanguageTeam = "TranScene",
                }
            };

            string currentSaga = "bb";

            foreach (string entry in source.Text)
            {
                string sentence = entry;
                if (string.IsNullOrEmpty(sentence))
                {
                    sentence = "<!empty>";
                }

                poExport.Add(new PoEntry(sentence) { Context = j.ToString() });
                j++;

                if (sagaEnd.Contains(sentence))
                {
                    
                    container.Root.Add(new Node(currentSaga, poExport));
                    poExport = new Po
                    {
                        Header = new PoHeader("Jump Ultimate Stars", "TranScene", "es")
                        {
                            LanguageTeam = "TranScene",
                        }
                    };
                    i++;
                    Console.WriteLine(i + sentence);
                    currentSaga = CleanName(sagaEnd[i]);
                }

            }

            container.Root.Add(new Node(currentSaga, poExport));


            return container;
        }

        private string CleanName(string sentence)
        {
            return sentence.Remove(sentence.Length - 2);
        }

        /*
        private void SetSagas()
        {
            sagas.Add("gt", "gitama");
            sagas.Add("cb", "cobra");
            sagas.Add("is", "Is");
            sagas.Add("ig", "Ichigo 100%");
            sagas.Add("ct", "Oliver y Benji");
            sagas.Add("nk", "Ninku");
            sagas.Add("tz", "taizo mote king saga");
            sagas.Add("yo", "yugioh");
            sagas.Add("nn", "neuro");
            sagas.Add("na", "naruto");
            sagas.Add("nb", "nube, maestro del infierno");
            sagas.Add("tr", "reborn!");
            sagas.Add("to", "prince of tennis");
            sagas.Add("tl", "Tottemo! Lucky Man");
            sagas.Add("es", "eyesield 21");
            sagas.Add("rb", "rokudenashi blues");
            sagas.Add("hs", "Houshing Engi");
            sagas.Add("pj", "Pyuu to Fuku! Jaguar");
            sagas.Add("tc", "Jungle King Tar-chan");
            sagas.Add("rk", "Rurouni Kenshin");
            sagas.Add("dn", "death note");
            sagas.Add("yh", "yuyu");
            sagas.Add("bb", "bobobo");
            sagas.Add("bc", "black cat");
            sagas.Add("bl", "bleach");
            sagas.Add("dg", "d. gray man");
            sagas.Add("db", "dragon ball");
            sagas.Add("hk", "Hokuto no Ken");
            sagas.Add("hh", "hunter x hunter");
            sagas.Add("jj", "jojo bizarre");
            sagas.Add("dt", "Komas ");
            sagas.Add("ds", "Dr.Slump ");
            sagas.Add("oj", "Sakigake!! Otokujuku");
            sagas.Add("bu", "Busou Renkin");
            sagas.Add("kk", "kochikame");
            sagas.Add("mo", "Midori no Makibaoh");
            sagas.Add("kn", "Kinnikuman");
            sagas.Add("sk", "shaman king");
            sagas.Add("ss", "caballeros del zodiaco");
            sagas.Add("mr", "Muhyo to Rouji");
            sagas.Add("sd", "Slam Dunk");
            sagas.Add("op", "one piece ");
        }
        */

        private void SetSagaEnd()
        {
            sagaEnd.Add("bb07");
            sagaEnd.Add("bc04");
            sagaEnd.Add("bl11");
            sagaEnd.Add("bu04");
            sagaEnd.Add("cb01");
            sagaEnd.Add("ct06");
            sagaEnd.Add("db12");
            sagaEnd.Add("dg04");
            sagaEnd.Add("dn08");
            sagaEnd.Add("ds04");
            sagaEnd.Add("es09");
            sagaEnd.Add("gt09");
            sagaEnd.Add("hh08");
            sagaEnd.Add("hk12");
            sagaEnd.Add("hs04");
            sagaEnd.Add("ig01");
            sagaEnd.Add("is01");
            sagaEnd.Add("jj09");
            sagaEnd.Add("kk11");
            sagaEnd.Add("kn14");
            sagaEnd.Add("mo02");
            sagaEnd.Add("mr09");
            sagaEnd.Add("na11");
            sagaEnd.Add("nb02");
            sagaEnd.Add("nk02");
            sagaEnd.Add("nn07");
            sagaEnd.Add("oj09");
            sagaEnd.Add("op12");
            sagaEnd.Add("pj07");
            sagaEnd.Add("rb01");
            sagaEnd.Add("rk06");
            sagaEnd.Add("sd04");
            sagaEnd.Add("sk04");
            sagaEnd.Add("ss11");
            sagaEnd.Add("tc01");
            sagaEnd.Add("tl02");
            sagaEnd.Add("to09");
            sagaEnd.Add("tr07");
            sagaEnd.Add("tz04");
            sagaEnd.Add("yh07");
            sagaEnd.Add("yo05");
        }
    }
}
