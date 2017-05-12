using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public static class MoveGenerator
    {
        public static List<Move> GenerateMovesForPiece(int piece, int[] board)
        {
            PieceType pt = Util.GetPieceType(piece);
            List<int> temp = new List<int>();
            List<Move> result = new List<Move>();
            switch (pt)
            {
                case PieceType.Pawn:
                    temp = PawnMoveGenerator.PossiblePositions(piece, board);
                    break;
                case PieceType.Night:
                    temp = KnightMoveGenerator.PossiblePositions(piece, board);
                    break;
                case PieceType.Bishop:
                    temp = BishopMoveGenerator.PossiblePositions(piece, board);
                    break;
                case PieceType.Rook:
                    temp = RookMoveGenerator.PossiblePositions(piece, board);
                    break;
                case PieceType.Queen:
                    temp = QueenMoveGenerator.PossiblePositions(piece, board);
                    break;
                case PieceType.King:
                    temp = KingMoveGenerator.PossiblePositions(piece, board);
                    break;
                default:
                    break;

            }

            foreach (int m in temp)
            {
                result.Add(new Move(piece, m));
            }
            return result;
        }
    }
}
