using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public static class RookMoveGenerator
    {
        public static ulong PseudomoveBitboard(int piece)
        {
            ulong result = 0;
            int position = Util.GetPieceOffset(piece);
            int posX = Util.GetXForPosition(position);
            int posY = Util.GetYForPosition(position);
            int v;
            ulong one = 1;
            //expand out from piece position
            for (int i = 1; i < 8; i++)
            {
                v = (position + (8 * i));//up
                if (Util.GetXForPosition(v) == posX && Util.GetYForPosition(v) < 8)
                {
                    result |= (one << (v));
                }

                v = (position - (8 * i));//down
                if (Util.GetXForPosition(v) == posX && Util.GetYForPosition(v) >= 0)
                {
                    result |= (one << (v));
                }

                v = (position + (i));//right
                if (Util.GetYForPosition(v) == posY && Util.GetXForPosition(v) < 8)
                {
                    result |= (one << (v));
                }

                v = (position - (i));//left
                if (Util.GetYForPosition(v) == posY && Util.GetXForPosition(v) >= 0)
                {
                    result |= (one << (v));
                }
            }
            return result;
        }
        public static List<int> PossiblePositions(int piece, int[] board)
        {
            bool isWhite = Util.IsWhite(piece);
            List<int> result = new List<int>();
            int position = Util.GetPieceOffset(piece);
            int unpositionedPiece = (piece - position);
            int posX = Util.GetXForPosition(position);
            int posY = Util.GetYForPosition(position);
            bool stopA = false, stopB = false, stopC = false, stopD = false;
            int v;
            //expand out from piece position
            for (int i = 1; i < 8; i++)
            {

                if (!stopA)
                {
                    v = (position + (8 * i));//up
                    if (Util.GetXForPosition(v) == posX && Util.GetYForPosition(v) < 8 && !stopA)
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
                    if (Util.GetXForPosition(v) == posX && Util.GetYForPosition(v) >= 0)
                    {
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
                    if (Util.GetYForPosition(v) == posY && Util.GetXForPosition(v) < 8)
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
                    if (Util.GetYForPosition(v) == posY && Util.GetXForPosition(v) >= 0)
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
        public static ulong PossiblePositionsBitboard(int piece, int[] board)
        {
            bool isWhite = Util.IsWhite(piece);
            ulong result = 0;
            int position = Util.GetPieceOffset(piece);
            int posX = Util.GetXForPosition(position);
            int posY = Util.GetYForPosition(position);
            bool stopA = false, stopB = false, stopC = false, stopD = false;
            ulong one = 1;
            int v;
            //expand out from piece position
            for (int i = 1; i < 8; i++)
            {

                if (!stopA)
                {
                    v = (position + (8 * i));//up
                    if (Util.GetXForPosition(v) == posX && Util.GetYForPosition(v) < 8 && !stopA)
                    {
                        if (board[v] > 0)
                        {
                            stopA = true;
                            if (Util.IsWhite(board[v]) != isWhite)
                                result |= (one << (v));
                        }
                        else
                        {
                            result |= (one << (v));
                        }
                    }
                }

                if (!stopB)
                {
                    v = (position - (8 * i));//down
                    if (Util.GetXForPosition(v) == posX && Util.GetYForPosition(v) >= 0)
                    {
                        if (board[v] > 0)
                        {
                            stopB = true;
                            if (Util.IsWhite(board[v]) != isWhite)
                                result |= (one << (v));
                        }
                        else
                        {
                            result |= (one << (v));
                        }
                    }
                }
                if (!stopC)
                {
                    v = (position + (i));//right
                    if (Util.GetYForPosition(v) == posY && Util.GetXForPosition(v) < 8)
                    {
                        if (board[v] > 0)
                        {
                            stopC = true;
                            if (Util.IsWhite(board[v]) != isWhite)
                                result |= (one << (v));
                        }
                        else
                        {
                            result |= (one << (v));
                        }
                    }
                }
                if (!stopD)
                {
                    v = (position - (i));//left
                    if (Util.GetYForPosition(v) == posY && Util.GetXForPosition(v) >= 0)
                    {
                        if (board[v] > 0)
                        {
                            stopD = true;
                            if (Util.IsWhite(board[v]) != isWhite)
                                result |= (one << (v));
                        }
                        else
                        {
                            result |= (one << (v));
                        }
                    }
                }
            }
            return result;
        }
    }
}
