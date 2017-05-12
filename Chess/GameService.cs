﻿using Chess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{


    public class GameService
    {
        List<short> GameState;
        short[] StartBoard = new short[32]{
            704,577,642,771,836,645,582,711,    //white royal
            520,521,522,523,524,525,526,527,    //white pawns
            48,49,50,51,52,53,54,55,            //black pawns
            248,121,186,315,380,189,126,255     //black royal
        };
        public GameService()
        {
            GameState = new List<short>();
            short[] temp = new short[32];
        }
        public string GetPieceStringFromShort(short value)
        {
            return string.Format("{0}-{1}-{2}", Util.GetBitAtPosition(value, 9) ? "W" : "B", Util.GetPieceType(value), Util.GetPieceOffset(value));
        }
        public short GetShortFromPieceString(string pieceString)
        {
            string[] value = pieceString.Split('-');
            short result = 0;
            if (value[0] == "W")
            {
                result += 512;
            }
            PieceType pt = (PieceType)Enum.Parse(typeof(PieceType), value[1]);
            result += (byte)((byte)pt << 6);//convert pt to number with correct bitshift (*64)
            result += byte.Parse(value[2]);//position offset
            return result;
        }
        //GetValidTypeMoves-> PruneSameColorBlockingMoves -> PawnPrune -> PruneCausesCheckMoves
    }
    public enum PieceType : byte
    {
        Pawn, Night, Bishop, Rook, Queen, King
    };

    public static class Util
    {
        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        static Dictionary<char, short> files = new Dictionary<char, short>() { { 'a', 0 }, { 'b', 1 }, { 'c', 2 }, { 'd', 3 }, { 'e', 4 }, { 'f', 5 }, { 'g', 6 }, { 'h', 7 } };
        static char[] filesChar = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
        public static short FileToShort(char file)
        {
            return files[file];
        }
        public static char ShortToFile(short file)
        {
            return filesChar[file];
        }

        public static short DepositionPiece(short piece)
        {
            return (short)(piece >> 6 << 6);
        }
        public static short GetYForPosition(short position)
        {
            return (short)(position >> 3);
        }
        public static short GetYForPiece(short piece)
        {
            return (short)((piece & 63) >> 3);
        }
        public static short GetXForPosition(short position)
        {
            return (short)(position & 7);
        }
        public static short GetXForPiece(short piece)
        {
            return (short)((piece & 63) & 7);
        }
        public static short GetPieceOffset(short piece)
        {
            return (short)(piece & 63);
        }

        public static bool IsWhite(short piece) {
            //return ((piece >> 9) & 1) == 1;//use this if more bits than 512
            return (piece >> 9) ==1;
        }
        public static PieceType GetPieceType(short piece)
        {
            int a = piece % 512;
            a = (a >> 6);
            return (PieceType)a;
        }

        //first 3 bits are for piece type, but that is 8 values and we only have 6 pieces. Fill in the blanks with some character. 
        public static char[] PieceNames = new char[] { 'p', 'n', 'b', 'r', 'q', 'k','_','_', 'P', 'N', 'B', 'R', 'Q', 'K' };
        public static char[] PieceProperNames = new char[] { ' ', 'N', 'B', 'R', 'Q', 'K', '_', '_', ' ', 'N', 'B', 'R', 'Q', 'K' };
        public static Dictionary<char,short> PieceValues = new Dictionary<char, short> { {'p',-1},{ 'P', 1 }, { 'n', -3 }, { 'N', 3 }, { 'b', -3 }, { 'B', 3 }, { 'r', -5 }, { 'R', 5 }, { 'q', -9 }, { 'Q', 9 }, { 'k', 0 }, { 'K', 0 } };
        public static short[] PieceValuesIndexed = new short[] { -1,-3,-3,-5,-9,-999,0,0,1,3,3,5,9,999};
        public static char GetPieceName(short piece)
        {
            //offset the position bits from the piece, and use the lookup array. 3 bits for type, and the 4th bit is color. 
            return PieceNames[(piece >> 6)];
        }
        public static char GetPieceProperName(short piece)
        {
            //offset the position bits from the piece, and use the lookup array. 3 bits for type, and the 4th bit is color. 
            return PieceProperNames[(piece >> 6)];
        }
        public static short GetPieceValue(short piece)
        {
            return PieceValuesIndexed[(piece >> 6)];
        }
        public static bool IsBlackKing(short piece)
        {
            return (piece >> 6 << 6)==320;
        }
        public static bool IsWhiteKing(short piece)
        {
            return (piece >> 6 << 6) == 832;
        }
        public static bool GetBitAtPosition(short piece, byte pos)
        {
            return (piece & (1 << pos)) != 0;
        }

    }
  
}
