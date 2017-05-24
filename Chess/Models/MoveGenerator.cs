using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public static class MoveGenerator
    {
        public static ulong PseudomoveBitboard(int piece, int[] board)
        {
            PieceType pt = Util.GetPieceType(piece);
            switch (pt)
            {
                case PieceType.Pawn:
                    return PawnMoveGenerator.PseudomoveBitboard(piece, board);
                case PieceType.Night:
                    return KnightMoveGenerator.PseudomoveBitboard(piece);
                case PieceType.Bishop:
                    return BishopMoveGenerator.PseudomoveBitboard(piece);
                case PieceType.Rook:
                    return RookMoveGenerator.PseudomoveBitboard(piece);
                case PieceType.Queen:
                    return QueenMoveGenerator.PseudomoveBitboard(piece);
                case PieceType.King:
                    return KingMoveGenerator.PseudomoveBitboard(piece);
                default:
                    break;
            }

            return 0;
        }
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
        public static ulong GenerateMovesForPieceBitboard(int piece, int[] board)
        {
            PieceType pt = Util.GetPieceType(piece);
            switch (pt)
            {
                case PieceType.Pawn:
                    return PawnMoveGenerator.PossiblePositionsBitboard(piece, board);
                case PieceType.Night:
                    return KnightMoveGenerator.PossiblePositionsBitboard(piece, board);
                case PieceType.Bishop:
                    return BishopMoveGenerator.PossiblePositionsBitboard(piece, board);
                case PieceType.Rook:
                    return RookMoveGenerator.PossiblePositionsBitboard(piece, board);
                case PieceType.Queen:
                    return QueenMoveGenerator.PossiblePositionsBitboard(piece, board);
                case PieceType.King:
                    return KingMoveGenerator.PossiblePositionsBitboard(piece, board);
                default:
                    break;
            }
            return 0;
        }
    }
}
