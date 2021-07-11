﻿using JUSToolkit.Converters.Bin;
using JUSToolkit.Converters.Bin.Bgm;
using JUSToolkit.Converters.Bin.ChrB;
using JUSToolkit.Converters.Bin.Commwin;
using JUSToolkit.Converters.Bin.Deck;
using JUSToolkit.Converters.Bin.Demo;
using JUSToolkit.Converters.Bin.Info;
using JUSToolkit.Converters.Bin.JQuiz;
using JUSToolkit.Converters.Bin.Koma;
using JUSToolkit.Converters.Bin.Location;
using JUSToolkit.Converters.Bin.Piece;
using JUSToolkit.Converters.Bin.Rulemess;
using JUSToolkit.Converters.Bin.Stage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JUSToolkit.Formats.Bin
{
    public static class DataFactory
    {
        public static JusData readData(DataType type)
        {
            switch (type) {
                case DataType.Bgm:
                    return new BgmData();
                case DataType.ChrB:
                    return new ChrBData();
                case DataType.Commwin:
                    return new CommwinData();
                case DataType.Deck:
                    return new DeckData();
                case DataType.Demo:
                    return new DemoData();
                case DataType.Info:
                    return new InfoData();
                case DataType.JQuiz:
                    return new JquizData();
                case DataType.Koma:
                    return new KomaData();
                case DataType.Location:
                    return new LocationData();
                case DataType.Piece:
                    return new PieceData();
                case DataType.Rulemess:
                    return new RulemessData();
                case DataType.Stage:
                    return new StageData();
                default:
                    return null;
            }
        }

        public static JusFormat createFormat(DataType type)
        {
            switch (type) {
                case DataType.Bgm:
                    return new Bgm();
                case DataType.ChrB:
                    return new ChrB();
                case DataType.Commwin:
                    return new Commwin();
                case DataType.Piece:
                    return new Piece();
                case DataType.Deck:
                    return new Deck();
                case DataType.Demo:
                    return new Demo();
                case DataType.Info:
                    return new Info();
                default:
                    return null;
            }
        }
    }

    public enum DataType
    {
        Bgm,
        ChrB,
        Commwin,
        Deck,
        Demo,
        Info,
        JQuiz,
        Koma,
        Location,
        Piece,
        Rulemess,
        Stage
    }
}
