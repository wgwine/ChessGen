using Chess.Models;
using System;
using System.Collections.Generic;

namespace Chess
{
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
        static Dictionary<char, int> files = new Dictionary<char, int>() { { 'a', 0 }, { 'b', 1 }, { 'c', 2 }, { 'd', 3 }, { 'e', 4 }, { 'f', 5 }, { 'g', 6 }, { 'h', 7 } };
        static char[] filesChar = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
        public static int FileToInt(char file)
        {
            return files[file];
        }
        public static char IntToFile(int file)
        {
            return filesChar[file];
        }

        public static int DepositionPiece(int piece)
        {
            return (piece >> 6 << 6);
        }
        public static int GetYForPosition(int position)
        {
            return (position >> 3);
        }
        public static int GetYForPiece(int piece)
        {
            return ((piece & 63) >> 3);
        }
        public static int GetXForPosition(int position)
        {
            return position & 7;
        }
        public static int GetXForPiece(int piece)
        {
            return (piece & 63) & 7;
        }
        public static int GetPieceOffset(int piece)
        {
            return piece & 63;
        }

        public static bool IsWhite(int piece) {
            //return ((piece >> 9) & 1) == 1;//use this if more bits than 512
            return (piece >> 9) == 1;
        }
        public static PieceType GetPieceType(int piece)
        {
            int a = piece % 512;
            a = (a >> 6);
            return (PieceType)a;
        }

        //first 3 bits are for piece type, but that is 8 values and we only have 6 pieces. Fill in the blanks with some character. 
        public static char[] PieceNames = new char[] { 'p', 'n', 'b', 'r', 'q', 'k','_','_', 'P', 'N', 'B', 'R', 'Q', 'K' };
        public static char[] PieceProperNames = new char[] { ' ', 'N', 'B', 'R', 'Q', 'K', '_', '_', ' ', 'N', 'B', 'R', 'Q', 'K' };
        public static Dictionary<char, int> PieceValues = new Dictionary<char, int> { {'p',-1},{ 'P', 1 }, { 'n', -3 }, { 'N', 3 }, { 'b', -3 }, { 'B', 3 }, { 'r', -5 }, { 'R', 5 }, { 'q', -9 }, { 'Q', 9 }, { 'k', 0 }, { 'K', 0 } };
        public static int[] PieceValuesIndexed = new int[] { -100,-300,-300,-500,-900,-9000,0,0,100,300,300,500,900,9000};
        public static char GetPieceName(int piece)
        {
            //offset the position bits from the piece, and use the lookup array. 3 bits for type, and the 4th bit is color. 
            return PieceNames[(piece >> 6)];
        }
        public static char GetPieceProperName(int piece)
        {
            //offset the position bits from the piece, and use the lookup array. 3 bits for type, and the 4th bit is color. 
            return PieceProperNames[(piece >> 6)];
        }
        public static int GetPieceValue(int piece)
        {
            return PieceValuesIndexed[(piece >> 6)];
        }
        public static bool IsKing(int piece)
        {
            return (piece >> 6 & 7) == 5;
        }
        public static bool IsBlackKing(int piece)
        {
            return (piece >> 6 << 6)==320;
        }
        public static bool IsWhiteKing(int piece)
        {
            return (piece >> 6 << 6) == 832;
        }
        public static bool GetBitAtPosition(int piece, byte pos)
        {
            return (piece & (1 << pos)) != 0;
        }
    }  
}
