using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{

    public interface IMoveGenerator
    {
        List<short> PossiblePositions(short piece);
    }
    public static class PawnMoveGenerator
    {
        public static List<short> PossiblePositions(short piece, List<short> board)
        {
            int colorMatch = Util.IsWhite(piece) ? 1 : 2;
            List<short> result = new List<short>();
            short position = Util.GetPieceOffset(piece);
            short positionX = Util.GetXForPosition(position);
            short positionY = Util.GetYForPosition(position);
            short move;
            //if white
            if (colorMatch==1)
            {
                if (positionY < 6)
                {
                    if (board[position + 8] == 0)
                        result.Add((short)(piece + 8));
                    //captures
                    if (board[position + 7] > 0 && board[position + 7] != colorMatch)
                        result.Add((short)(piece + 7));
                    if (board[position + 9] > 0 && board[position + 9] != colorMatch)
                        result.Add((short)(piece + 9));

                    if (positionY == 1)
                    {
                        if (board[position + 8] == 0 && board[position + 16] == 0)
                            result.Add((short)(piece + 16));
                    }
                }else
                {
                    //pawn promotion
                    result.Add((short)(PieceTypeFENMap.PieceValue('Q') + position + 8));
                }
            }
            else
            {
                if (positionY > 1)
                {
                    if (board[position - 8] == 0)
                        result.Add((short)(piece - 8));

                    if (board[position - 7] > 0 && board[position - 7] != colorMatch)
                        result.Add((short)(piece - 7));

                    if (board[position - 9] > 0 && board[position - 9] != colorMatch)
                        result.Add((short)(piece - 9));

                    if (positionY == 6)
                    {
                        if (board[position - 8] == 0 && board[position - 16] == 0)
                            result.Add((short)(piece - 16));
                    }
                }
                else
                {
                    result.Add((short)(PieceTypeFENMap.PieceValue('q')+ position - 8));
                    //pawn promotion
                }
            }
            return result;
        }
    }
}
