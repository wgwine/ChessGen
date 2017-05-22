using System;
using System.Collections.Generic;

namespace Chess.Models
{
    public static class QueenMoveGenerator 
    {
        public static ulong PseudomoveBitboard(int piece)
        {
            return BishopMoveGenerator.PseudomoveBitboard(piece) | RookMoveGenerator.PseudomoveBitboard(piece);
        }
        public static List<int> PossiblePositions(int piece, int[] board)
        {
            List<int> result = BishopMoveGenerator.PossiblePositions(piece, board);
            result.AddRange(RookMoveGenerator.PossiblePositions(piece, board));
            return result;
        }
    }
}
