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
        public List<short> GenerateMovesForPiece(short piece)
        {
            PieceType pt = Util.GetPieceType(piece);
            switch (pt)
            {
                case PieceType.Pawn:
                    return PawnMoveGenerator.PossiblePositions(piece);
                case PieceType.Night:
                    return KnightMoveGenerator.PossiblePositions(piece);
                case PieceType.Bishop:
                    return BishopMoveGenerator.PossiblePositions(piece);
                case PieceType.Rook:
                    return RookMoveGenerator.PossiblePositions(piece);
                case PieceType.Queen:
                    return QueenMoveGenerator.PossiblePositions(piece);
                case PieceType.King:
                    return KingMoveGenerator.PossiblePositions(piece);
                default:
                    break;

            }
            return PawnMoveGenerator.PossiblePositions(piece);
        }
    }
}
