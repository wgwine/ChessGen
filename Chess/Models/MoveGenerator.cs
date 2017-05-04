using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public class MoveGenerator
    {
        public MoveGenerator()
        {

        }
        public List<short> GenerateMovesForPiece(short piece, List<short> board)
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
    }
}
