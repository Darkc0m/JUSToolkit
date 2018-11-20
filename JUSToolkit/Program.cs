﻿namespace JUSToolkit
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
    using Texim.Media.Image;
    using Texim.Media.Image.Processing;

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
                showUsage();
            }
            else
            {
                showCredits();

                string type = args[0];
                string inputFileName = args[1];
                string outputPath = args[2];

                if(args[0] == "-i"){
                    string fileToInsert = args[3];
                }

                Identify i = new Identify();

                log.Info("Identifying file " + inputFileName);

                Node n = NodeFactory.FromFile(inputFileName);

                Format inputFormat = i.GetFormat(n);

                // Compressed file
                if(inputFormat.ToString() == FORMATPREFIX + "DSCP"){
                    n = Utils.DecompressLzss(n);
                    inputFormat = i.GetFormat(n);
                }

                log.Info("Format detected: " + inputFormat.ToString());

                switch(type){
                    case "-e":
                        Export(inputFormat.ToString(), n, outputPath, inputFileName);
                    break;
                }

                log.Info("Program completed.");

            }
        }

        private static void Export(string format, Node n, string outputPath, string inputFileName){
            switch (format)
            {
                case FORMATPREFIX + "BinTutorial":

                    n.Transform<BinaryFormat2BinTutorial, BinaryFormat, BinTutorial>()
                    .Transform<Bin2Po, BinTutorial, Po>()
                    .Transform<Po2Binary, Po, BinaryFormat>()
                    .Stream.WriteTo(outputPath + Path.PathSeparator + inputFileName + ".po");

                    break;

                case FORMATPREFIX + "ALAR3":

                    var folder = n.Transform<BinaryFormat2Alar3, BinaryFormat, ALAR3>()
                        .Transform<Alar2Nodes, ALAR3, NodeContainerFormat>();

                    SaveToDir(folder, outputPath);

                    break;

                //case FORMATPREFIX + "ALAR2":

                    //var root = n.Transform<BinaryFormat2Alar2, BinaryFormat, ALAR2>()
                    //    .Transform<Alar2Nodes, ALAR3, NodeContainerFormat>();

                    //SaveToDir(root, outputPath);

                    //break;

                case FORMATPREFIX + "DIG":

                    DIG dig = n.Transform<Binary2DIG, BinaryFormat, DIG>().GetFormatAs<DIG>();

                    var img = dig.Pixels.CreateBitmap(dig.Palette, 0);

                    img.Save(inputFileName + ".png");

                    break;

            }
            log.Info("Transformado " + n.Name);

            log.Info("Guardada exportación en " + outputPath);
        }

        private static void showUsage()
        {
            log.Info("Usage: JUSToolkit.exe -e <fileToExtract> <dirToExtractTo>");
            log.Info("Usage: JUSToolkit.exe -i <originalFile> <dirToExtractTo> <fileToInsert>");
        }

        private static void showCredits()
        {
            log.Info("=========================");
            log.Info("== JUS TOOLKIT by Nex ==");
            log.Info("=========================");
        }

        private static void SaveToDir(Node folder, string output){
            Directory.CreateDirectory(output);
            foreach (var child in folder.Children)
            {
                string outputFile = Path.Combine(output, child.Name);
                log.Info("Guardando " + outputFile);
                child.GetFormatAs<BinaryFormat>().Stream.WriteTo(outputFile);
            }
        }
    }
}
