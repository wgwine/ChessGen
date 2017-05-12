using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public static class RookMoveGenerator
    {

        public static List<short> PossiblePositions(short piece, List<short> board)
        {
            bool isWhite = Util.IsWhite(piece);
            List<short> result = new List<short>();
            short position = Util.GetPieceOffset(piece);
            short unpositionedPiece = (short)(piece - position);
            short R = (short)(Util.GetYForPosition(position) * 8);

            short posX = Util.GetXForPosition(position);
            short posY = Util.GetYForPosition(position);
            bool stopA = false, stopB = false, stopC = false, stopD = false;

            //expand out from piece position
            for (int i = 1; i < 8; i++)
            {
                short a = (short)(position + (8 * i));//up
                short b = (short)(position - (8 * i));//down
                short c = (short)(position + (i));//right
                short d = (short)(position - (i));//left

                if (Util.GetXForPosition(a) == posX && Util.GetYForPosition(a) < 8 && !stopA)
                {
                    var newPos = (short)(unpositionedPiece + a);
                    if (board[a] > 0)
                    {
                        stopA = true;
                        if (Util.IsWhite(board[a]) != isWhite)
                            result.Add((short)(unpositionedPiece + a));
                    }
                    else
                    {
                        result.Add((short)(unpositionedPiece + a));
                    }
                }
                if (Util.GetXForPosition(b) == posX && Util.GetYForPosition(b) >=0 && !stopB)
                {
                    var newPos = (short)(unpositionedPiece + b);
                    if (board[b] > 0)
                    {
                        stopB = true;
                        if (Util.IsWhite(board[b]) != isWhite)
                            result.Add((short)(unpositionedPiece + b));
                    }
                    else
                    {
                        result.Add((short)(unpositionedPiece + b));
                    }
                }
                if (Util.GetYForPosition(c) == posY && Util.GetXForPosition(c) < 8 && !stopC)
                {
                    var newPos = (short)(unpositionedPiece + c);
                    if (board[c] > 0)
                    {
                        stopC = true;
                        if (Util.IsWhite(board[c]) != isWhite)
                            result.Add((short)(unpositionedPiece + c));
                    }
                    else
                    {
                        result.Add((short)(unpositionedPiece + c));
                    }
                }
                if (Util.GetYForPosition(d) == posY && Util.GetXForPosition(d) >= 0  && !stopD)
                {
                    var newPos = (short)(unpositionedPiece + d);
                    if (board[d] > 0)
                    {
                        stopD = true;
                        if (Util.IsWhite(board[d]) != isWhite)
                            result.Add((short)(unpositionedPiece + d));
                    }
                    else
                    {
                        result.Add((short)(unpositionedPiece + d));
                    }
                }
            }

            return result;
        }
    }
}
