using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public static class RookMoveGenerator
    {

        public static List<int> PossiblePositions(int piece, int[] board)
        {
            bool isWhite = Util.IsWhite(piece);
            List<int> result = new List<int>();
            int position = Util.GetPieceOffset(piece);
            int unpositionedPiece = (piece - position);
            int R = (Util.GetYForPosition(position) * 8);

            int posX = Util.GetXForPosition(position);
            int posY = Util.GetYForPosition(position);
            bool stopA = false, stopB = false, stopC = false, stopD = false;
            int x, y, v;
            //expand out from piece position
            for (int i = 1; i < 8; i++)
            {

                if (!stopA)
                {
                    v = (position + (8 * i));//up
                    x = Util.GetXForPosition(v);
                    y = Util.GetYForPosition(v);
                    if (x == posX && y < 8 && !stopA)
                    {
                        if (board[v] > 0)
                        {
                            stopA = true;
                            if (Util.IsWhite(board[v]) != isWhite)
                                result.Add((unpositionedPiece + v));
                        }
                        else
                        {
                            result.Add((unpositionedPiece + v));
                        }
                    }
                }

                if (!stopB)
                {
                    v = (position - (8 * i));//down
                    x = Util.GetXForPosition(v);
                    y = Util.GetYForPosition(v);

                    if (x == posX && y >= 0)
                    {
                        var newPos = (unpositionedPiece + v);
                        if (board[v] > 0)
                        {
                            stopB = true;
                            if (Util.IsWhite(board[v]) != isWhite)
                                result.Add((unpositionedPiece + v));
                        }
                        else
                        {
                            result.Add((unpositionedPiece + v));
                        }
                    }
                }
                if (!stopC)
                {
                    v = (position + (i));//right
                    x = Util.GetXForPosition(v);
                    y = Util.GetYForPosition(v);
                    if (y == posY && x < 8)
                    {
                        if (board[v] > 0)
                        {
                            stopC = true;
                            if (Util.IsWhite(board[v]) != isWhite)
                                result.Add((unpositionedPiece + v));
                        }
                        else
                        {
                            result.Add((unpositionedPiece + v));
                        }
                    }
                }
                if (!stopD)
                {
                    v = (position - (i));//left
                    x = Util.GetXForPosition(v);
                    y = Util.GetYForPosition(v);
                    if (y == posY && x >= 0)
                    {
                        if (board[v] > 0)
                        {
                            stopD = true;
                            if (Util.IsWhite(board[v]) != isWhite)
                                result.Add((unpositionedPiece + v));
                        }
                        else
                        {
                            result.Add((unpositionedPiece + v));
                        }
                    }
                }
            }
            return result;
        }
    }
}
