namespace JUSToolkit
{
    using System;
    using Yarhl.FileFormat;
    using log4net;
    using log4net.Config;
    using Formats;
    using Converters.Bin;
    using Converters.Alar;
    using Converters.Images;
    using Yarhl.FileSystem;
    using Yarhl.Media.Text;
    using System.IO;
    using JUSToolkit.Formats.ALAR;
    using System.Drawing;
    using Texim.Media.Image.Processing;
    using Texim.Media.Image;
    using System.Text;
    using System.Collections.Generic;

    class MainClass
    {
        public static readonly ILog log = LogManager.GetLogger(typeof(MainClass));
        private const String FORMATPREFIX = "JUSToolkit.Formats.";

        public static void Main(string[] args)
        {
            BasicConfigurator.Configure();

            if (args.Length < 3 || args.Length > 4)
            {
                log.Error("Wrong arguments.");
                ShowUsage();
            }
            else
            {
                ShowCredits();

                string type = args[0];
                string inputFileName = args[1];
                string dirToSave = args[2];
                string dataToInsert = null;

                // inputFilename - dirtosave - (dir/file to insert)

                if (args[0] != "-e"){
                    dataToInsert = args[3];
                }

                Node n;
                if (inputFileName.EndsWith(".po"))
                    inputFileName = "po";

                switch (inputFileName)
                {
                    case ".":
                        ProcessDir(type, dirToSave, dataToInsert);
                        break;

                    case "po":
                        n = NodeFactory.FromFile(args[1]);
                        ProcessDir(type, n, dirToSave, dataToInsert);
                        break;

                    default:
                        log.Info("Identifying file " + inputFileName);

                        n = NodeFactory.FromFile(inputFileName);
                        ProcessFile(type, n, dirToSave, dataToInsert);
                        break;

                }
                /*if (inputFileName == ".")
                {
                    ProcessDir(type, dirToSave, dataToInsert);
                }
                else
                {
                    log.Info("Identifying file " + inputFileName);

                    Node n = NodeFactory.FromFile(inputFileName);
                    ProcessFile(type, n, dirToSave, dataToInsert);
                }*/

                log.Info("Program completed.");

            }
        }

        private static void ProcessDir(string type, string dirToSave, string dataToInsert)
        {
            log.Info("Exporting...");
            switch (type)
            {
                case "-exportdig":

                    Node digContainer = NodeFactory.FromDirectory(dataToInsert, "*.dig");

                    foreach (Node n in digContainer.Children) 
                    {
                        ProcessFile("-e", n, dirToSave, dataToInsert);
                    }

                    break;

                case "-exportBinDeck":

                    using (Node container = NodeFactory.FromDirectory(dataToInsert, "*.bin"))
                    {
                        string fileName = container.Children[0].Name.Remove(9) + ".po";
                        container.Transform<BinDeck2Nodes, NodeContainerFormat, BinDeck>()
                            .Transform<BinDeckGeneric2Po, BinDeck, Po>()
                            .Transform<Po2Binary, Po, BinaryFormat>()
                            .Stream.WriteTo(Path.Combine(dirToSave, fileName));
                    }

                    break;

                case "-exportBinDeck_P":

                    using (Node container = NodeFactory.FromDirectory(dataToInsert, "*.bin"))
                    {
                        string fileName = container.Children[0].Name.Remove(9) + "_P.po";
                        container.Transform<BinDeckP2Nodes, NodeContainerFormat, BinDeckP>()
                            .Transform<BinDeckGeneric2Po, BinDeckP, Po>()
                            .Transform<Po2Binary, Po, BinaryFormat>()
                            .Stream.WriteTo(Path.Combine(dirToSave, fileName));
                    }

                    break;
            }
        }

        private static void ProcessDir(string type, Node n, string dirToSave, string dataToInsert)
        {
            switch (type)
            {
                case "-importDeck":

                    log.Info("Importing...");

                    using (Node container = NodeFactory.FromDirectory(dataToInsert, "*.bin"))
                    {
                        string sentence;
                        List<string> updatedText;
                        n.Stream.Position = 0;
                        Po po = n.Transform<Po2Binary, BinaryFormat, Po>().GetFormatAs<Po>();
                        foreach (Node deck in container.Children)
                        {
                            updatedText = new List<string>();
                            var binD = deck.Transform<Binary2BinDeck, BinaryFormat, BinDeck>().GetFormatAs<BinDeck>();

                            foreach (PoEntry entry in po.Entries)
                            {
                                if (deck.Name.Equals(entry.Context))
                                {
                                    sentence = entry.Text;
                                    if (sentence.Equals("<!empty>"))
                                        sentence = string.Empty;
                                    updatedText.Add(sentence);
                                }

                            }
                            binD.Text = updatedText;
                            log.Info("Importing file" + deck.Name);
                            binD.ConvertWith<BinDeck2Binary, BinDeck, BinaryFormat>()
                                .Stream.WriteTo(Path.Combine(dirToSave, deck.Name.Remove(deck.Name.Length - 4) + "_new.bin"));
                        }
                    }

                    break;

                case "-importBinDeck_P":

                    log.Info("Importing...");

                    using (Node container = NodeFactory.FromDirectory(dataToInsert, "*.bin"))
                    {
                        string sentence;
                        List<string> updatedText;
                        n.Stream.Position = 0;
                        Po po = n.Transform<Po2Binary, BinaryFormat, Po>().GetFormatAs<Po>();
                        foreach (Node deck in container.Children)
                        {
                            updatedText = new List<string>();
                            var binD = deck.Transform<Binary2BinDeckP, BinaryFormat, BinDeckP>().GetFormatAs<BinDeckP>();

                            foreach (PoEntry entry in po.Entries)
                            {
                                if (deck.Name.Equals(entry.Context))
                                {
                                    sentence = entry.Text;
                                    if (sentence.Equals("<!empty>"))
                                        sentence = string.Empty;
                                    updatedText.Add(sentence);
                                }

                            }
                            binD.Text = updatedText;
                            log.Info("Importing file" + deck.Name);
                            binD.ConvertWith<BinDeckP2Binary, BinDeckP, BinaryFormat>()
                                .Stream.WriteTo(Path.Combine(dirToSave, deck.Name.Remove(deck.Name.Length - 4) + "_new.bin"));
                        }
                    }

                    break;
            }
        }

        private static void ProcessFile(string type, Node n, string dirToSave, string dataToInsert)
        {
            Identify i = new Identify();

            Format inputFormat = i.GetFormat(n);

            // Compressed file
            if (inputFormat.ToString() == FORMATPREFIX + "DSCP")
            {
                BinaryFormat binary = Utils.Lzss(new BinaryFormat(n.Stream), "-d");
                n = new Node(n.Name, binary);
                inputFormat = i.GetFormat(n);
            }

            log.Info("Format detected: " + inputFormat.ToString());

            switch (type)
            {// inputFilename - dirtosave - (dir/file to insert)
                case "-e":
                    Export(inputFormat.ToString(), n, dirToSave);
                    break;

                case "-i":
                    Import(inputFormat.ToString(), n, dirToSave, dataToInsert);
                    break;

            }
        }

        private static void Export(string format, Node n, string outputPath){

            log.Info("Exporting...");

            switch (format)
            {
                case FORMATPREFIX + "BinTutorial":
                case FORMATPREFIX + "BinInfoTitle":

                    n.Transform<Binary2Bin, BinaryFormat, Bin>()
                    .Transform<Bin2Po, Bin, Po>()
                    .Transform<Po2Binary, Po, BinaryFormat>()
                    .Stream.WriteTo(Path.Combine(outputPath, n.Name + ".po"));

                    break;

                case FORMATPREFIX + "BinDeck":

                    Console.WriteLine(n.Path);


                    break;

                case FORMATPREFIX + "BinDeckP":

                    n.Transform<Binary2BinDeckP, BinaryFormat, BinDeckP>()
                    .Transform<BinDeckP2Po, BinDeckP, Po>()
                    .Transform<Po2Binary, Po, BinaryFormat>()
                    .Stream.WriteTo(Path.Combine(outputPath, n.Name + ".po"));

                    break;

                case FORMATPREFIX + "BinGBattleMission":

                    n.Transform<Binary2BinGBattleMission, BinaryFormat, BinGBattleMission>()
                    .Transform<Bin2Po, Bin, Po>()
                    .Transform<Po2Binary, Po, BinaryFormat>()
                    .Stream.WriteTo(Path.Combine(outputPath, n.Name + ".po"));

                    break;

                case FORMATPREFIX + "BinGGalaxy":

                    n.Transform<Binary2BinGGalaxy, BinaryFormat, BinGGalaxy>()
                    .Transform<Bin2Po, Bin, Po>()
                    .Transform<Po2Binary, Po, BinaryFormat>()
                    .Stream.WriteTo(Path.Combine(outputPath, n.Name + ".po"));

                    break;

                case FORMATPREFIX + "BinQuiz":

                    var quizs = n.Transform<Binary2Bin, BinaryFormat, Bin>()
                    .Transform<Bin2JQuizContainer, Bin, NodeContainerFormat>();

                    foreach (Node po in quizs.Children) {
                        string outputFile = Path.Combine(outputPath, po.Name + ".po");
                        log.Info("Saving " + outputFile);
                        po.Transform<Po2Binary, Po, BinaryFormat>()
                        .Stream.WriteTo(outputFile);
                    }

                    break;

                case FORMATPREFIX + "ALAR.ALAR3":

                    var folder = n.Transform<BinaryFormat2Alar3, BinaryFormat, ALAR3>()
                        .Transform<Alar3ToNodes, ALAR3, NodeContainerFormat>();

                    SaveToDir(folder, outputPath);

                    break;

                case FORMATPREFIX + "ALAR.ALAR2":

                    var root = n.Transform<BinaryFormat2Alar2, BinaryFormat, ALAR2>()
                        .Transform<Alar2ToNodes, ALAR2, NodeContainerFormat>();

                    SaveToDir(root, outputPath);

                    break;

                case FORMATPREFIX + "DIG":

                    DIG dig = n.Transform<Binary2DIG, BinaryFormat, DIG>().GetFormatAs<DIG>();

                    var img = dig.Pixels.CreateBitmap(dig.Palette, 0);

                    img.Save(Path.Combine(outputPath, n.Name + ".png"));

                    break;

            }

            log.Info("Finished exporting");

        }

        private static void Import(string format, Node n, string dirToSave, string dataToInsert)
        {
            log.Info("Importing...");

            switch (format)
            {
                case FORMATPREFIX + "BinInfoTitle":

                    using(Node nodePo = NodeFactory.FromFile(dataToInsert))
                    {
                        Po2BinInfoTitle p2b = new Po2BinInfoTitle()
                        {
                            OriginalFile = new Yarhl.IO.DataReader(n.Stream)
                            {
                                DefaultEncoding = Encoding.GetEncoding(932)
                            }
                        };


                        nodePo.Transform<Po2Binary, BinaryFormat, Po>();
                        Node nodeBin = nodePo.Transform<Po, BinInfoTitle>(p2b)
                            .Transform<Bin2Binary, BinInfoTitle, BinaryFormat>();
                        nodeBin.Stream.WriteTo(Path.Combine(dirToSave, n.Name.Remove(n.Name.Length - 4) + "_new.bin"));
                    }

                    break;

                case FORMATPREFIX + "BinTutorial":

                    using (Node nodePo = NodeFactory.FromFile(dataToInsert))
                    {
                        Po2BinTutorial p2b = new Po2BinTutorial()
                        {
                            OriginalFile = new Yarhl.IO.DataReader(n.Stream)
                            {
                                DefaultEncoding = Encoding.GetEncoding(932)
                            }
                        };


                        nodePo.Transform<Po2Binary, BinaryFormat, Po>();
                        Node nodeBin = nodePo.Transform<Po, BinTutorial>(p2b)
                            .Transform<Bin2Binary, BinTutorial, BinaryFormat>();
                        nodeBin.Stream.WriteTo(Path.Combine(dirToSave, n.Name.Remove(n.Name.Length - 4) + "_new.bin"));
                    }


                    break;

                case FORMATPREFIX + "BinGBattleMission":

                    using (Node nodePo = NodeFactory.FromFile(dataToInsert))
                    {
                        Po2BinGBattleMission p2bg = new Po2BinGBattleMission()
                        {
                            OriginalFile = new Yarhl.IO.DataReader(n.Stream)
                            {
                                DefaultEncoding = Encoding.GetEncoding(932)
                            }
                        };

                        nodePo.Transform<Po2Binary, BinaryFormat, Po>()
                            .Transform<Po, BinGBattleMission>(p2bg)
                            .Transform<BinGBattleMission2Binary, BinGBattleMission, BinaryFormat>()
                            .Stream.WriteTo(Path.Combine(dirToSave, n.Name.Remove(n.Name.Length - 4) + "_new.bin"));
                    }

                    break;

                case FORMATPREFIX + "BinGGalaxy":

                    using (Node nodePo = NodeFactory.FromFile(dataToInsert))
                    {
                        Po2BinGGalaxy p2bg = new Po2BinGGalaxy()
                        {
                            OriginalFile = new Yarhl.IO.DataReader(n.Stream)
                            {
                                DefaultEncoding = Encoding.GetEncoding(932)
                            }
                        };

                        nodePo.Transform<Po2Binary, BinaryFormat, Po>()
                            .Transform<Po, BinGGalaxy>(p2bg)
                            .Transform<BinGGalaxy2Binary, BinGGalaxy, BinaryFormat>()
                            .Stream.WriteTo(Path.Combine(dirToSave, n.Name.Remove(n.Name.Length - 4) + "_new.bin"));
                    }

                    break;

                case FORMATPREFIX + "BinQuiz":

                    using(Node container = NodeFactory.FromDirectory(dataToInsert))
                    {
                        var N2BQ = new Nodes2BinQuiz()
                        {
                            OriginalFile = new Yarhl.IO.DataReader(n.Stream)
                            {
                                DefaultEncoding = Encoding.GetEncoding(932)
                            }
                        };

                        container.Transform<NodeContainerFormat, BinQuiz>(N2BQ)
                            .Transform<BinQuiz2Binary, BinQuiz, BinaryFormat>()
                            .Stream.WriteTo(Path.Combine(dirToSave, n.Name.Remove(n.Name.Length - 4) + "_new.bin"));
                    }

                    break;

                case FORMATPREFIX + "ALAR.ALAR3":

                    // Alar original
                    Node original = n.Transform<BinaryFormat2Alar3, BinaryFormat, ALAR3>();

                    // Contenedor con los ficheros a insertar
                    var newAlar = NodeFactory.FromDirectory(dataToInsert, "*.*")
                        .Transform<Alar3ToNodes, NodeContainerFormat, ALAR3>();

                    // Modificamos el Alar original con los nuevos ficheros a insertar
                    original.GetFormatAs<ALAR3>().InsertModification(newAlar.GetFormatAs<ALAR3>()); 

                    original.Transform<BinaryFormat2Alar3, ALAR3, BinaryFormat>()
                        .Stream.WriteTo(Path.Combine(dirToSave, n.Name.Remove(n.Name.Length - 4) + "_new.aar"));

                break;

                case FORMATPREFIX + "DIG":

                    DIG originalDig = n.Transform<Binary2DIG, BinaryFormat, DIG>().GetFormatAs<DIG>();

                    // Import the new PNG file
                    Bitmap newImage = (Bitmap)Image.FromFile(dataToInsert);
                    var quantization = new FixedPaletteQuantization(originalDig.Palette.GetPalette(0));
                    Texim.Media.Image.ImageConverter importer = new Texim.Media.Image.ImageConverter {
                        Format = ColorFormat.Indexed_4bpp,
                        PixelEncoding = PixelEncoding.HorizontalTiles,
                        Quantization = quantization
                    };

                    (Palette _, PixelArray pixelInfo) = importer.Convert(newImage);

                    originalDig.Pixels = pixelInfo;

                    BinaryFormat b = originalDig.ConvertWith<Binary2DIG, DIG, BinaryFormat>();

                    Utils.Lzss(b, "-evn").Stream.WriteTo(Path.Combine(dirToSave, n.Name + "evn.dig"));

                    break;
            }
            log.Info("Finished importing");
        }

        private static void ShowUsage()
        {
            log.Info("Usage: JUSToolkit.exe -e <fileToExtract> <dirToSave>");
            log.Info("Usage: JUSToolkit.exe -iDir <inputFileName> <dirToSave> <dirWithFilesToInsert>");
            log.Info("Usage: JUSToolkit.exe -i <inputFileName> <dirToSave> <fileToInsert>");
        }

        private static void ShowCredits()
        {
            log.Info("=========================");
            log.Info("== JUS TOOLKIT by Nex ==");
            log.Info("== Using YARHL by Pleonex ==");
            log.Info("=========================");
        }

        private static void SaveToDir(Node folder, string output){
            Directory.CreateDirectory(output);
            foreach (var child in folder.Children)
            {
                string outputFile = Path.Combine(output, child.Name);
                log.Info("Saving " + outputFile);
                child.GetFormatAs<BinaryFormat>().Stream.WriteTo(outputFile);
            }

            log.Info("Saved in " + output);
        }
    }
}
