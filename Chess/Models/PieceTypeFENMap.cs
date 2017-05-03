using System;
using System.Collections.Generic;

namespace Chess.Models
{
    public static class PieceTypeFENMap
    {
        public static Dictionary<char, int> valueMap = new Dictionary<char, int>() { { 'k', 320 }, { 'q', 256 }, { 'r', 192 }, { 'b', 128 }, { 'n', 64 }, { 'p', 0 } };
        public static int PieceValue(char v)
        {
            return valueMap[Char.ToLower(v)] + (Char.IsUpper(v)? 512 : 0);
        }
        public static char PieceName(short v)
        {
            return Util.GetPieceName(v);
        }
    }
}
