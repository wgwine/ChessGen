using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public static class MoveGenerator
    {
        public static List<short> GenerateMovesForPiece(short piece, List<short> board)
        {
            PieceType pt = Util.GetPieceType(piece);
            switch (pt)
            {
                case PieceType.Pawn:
                    return PawnMoveGenerator.PossiblePositions(piece, board);
                case PieceType.Night:
                    return KnightMoveGenerator.PossiblePositions(piece, board);
                case PieceType.Bishop:
                    return BishopMoveGenerator.PossiblePositions(piece, board);
                case PieceType.Rook:
                    return RookMoveGenerator.PossiblePositions(piece, board);
                case PieceType.Queen:
                    return QueenMoveGenerator.PossiblePositions(piece, board);
                case PieceType.King:
                    return KingMoveGenerator.PossiblePositions(piece, board);
                default:
                    break;

            }
            return new List<short>();
        }
        public static List<Move> GenerateMovesForPiece2(short piece, List<short> board)
        {
            PieceType pt = Util.GetPieceType(piece);
            List<short> temp = new List<short>();
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

            foreach (short m in temp)
            {
                result.Add(new Move(piece, m));
            }
            return result;
        }
    }
}
