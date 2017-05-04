using System;
using System.Collections.Generic;

namespace Chess.Models
{
    public static class QueenMoveGenerator 
    {
        public static List<short> PossiblePositions(short piece, List<short> board)
        {
            List<short> result = BishopMoveGenerator.PossiblePositions(piece, board);
            result.AddRange(RookMoveGenerator.PossiblePositions(piece, board));
            return result;
        }
    }
}
