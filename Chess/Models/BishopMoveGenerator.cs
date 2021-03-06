﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public static class BishopMoveGenerator
    {

        public static List<int> PossiblePositions(int piece, int[] board)
        {
            bool isWhite = Util.IsWhite(piece);
            List<int> result = new List<int>();
            int position = Util.GetPieceOffset(piece);
            int unpositionedPiece = Util.DepositionPiece(piece);
            int posX = Util.GetXForPosition(position);
            bool stopA = false, stopB = false, stopC = false, stopD = false;
            int x, y, a, b, c, d;
            for (int i = 1; i < 8; i++)
            {
                if (!stopA)
                {
                    a = (position + (9 * i));//up right
                    x = Util.GetXForPosition(a);
                    y = Util.GetYForPosition(a);
                    //prevent wrapping
                    if (x > posX && x < 8 && y < 8)
                    {
                        if (board[a] > 0)
                        {
                            stopA = true;
                            if (Util.IsWhite(board[a]) != isWhite)
                                result.Add((unpositionedPiece + a));
                        }
                        else
                        {
                            result.Add((unpositionedPiece + a));
                        }
                    }
                }
                if (!stopB)
                {
                    b = (position - (9 * i));//down left
                    x = Util.GetXForPosition(b);
                    y = Util.GetYForPosition(b);
                    if (x < posX && x >= 0 && y >= 0)
                    {
                        if (board[b] > 0)
                        {
                            stopB = true;
                            if (Util.IsWhite(board[b]) != isWhite)
                                result.Add((unpositionedPiece + b));
                        }
                        else
                        {
                            result.Add((unpositionedPiece + b));
                        }
                    }
                }
                if (!stopC)
                {
                    c = (position + (7 * i));//up left
                    x = Util.GetXForPosition(c);
                    y = Util.GetYForPosition(c);
                    if (x < posX && x >= 0 && y < 8)
                    {
                        if (board[c] > 0)
                        {
                            stopC = true;
                            if (Util.IsWhite(board[c]) != isWhite)
                                result.Add((unpositionedPiece + c));
                        }
                        else
                        {
                            result.Add((unpositionedPiece + c));
                        }
                    }
                }

                if (!stopD)
                {
                    d = (position - (7 * i));//down right
                    x = Util.GetXForPosition(d);
                    y = Util.GetYForPosition(d);
                    if (x > posX && x < 8 && y >= 0)
                    {
                        if (board[d] > 0)
                        {
                            stopD = true;
                            if (Util.IsWhite(board[d]) != isWhite)
                                result.Add((unpositionedPiece + d));
                        }
                        else
                        {
                            result.Add((unpositionedPiece + d));
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
            int unpositionedPiece = Util.DepositionPiece(piece);
            int posX = Util.GetXForPosition(position);
            bool stopA = false, stopB = false, stopC = false, stopD = false;
            int x, y, v;
            ulong one = 1;
            for (int i = 1; i < 8; i++)
            {
                if (!stopA)
                {
                    v = (position + (9 * i));//up right
                    x = Util.GetXForPosition(v);
                    y = Util.GetYForPosition(v);
                    //prevent wrapping
                    if (x > posX && x < 8 && y < 8)
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
                    v = (position - (9 * i));//down left
                    x = Util.GetXForPosition(v);
                    y = Util.GetYForPosition(v);
                    if (x < posX && x >= 0 && y >= 0)
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
                    v = (position + (7 * i));//up left
                    x = Util.GetXForPosition(v);
                    y = Util.GetYForPosition(v);
                    if (x < posX && x >= 0 && y < 8)
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
                    v = (position - (7 * i));//down right
                    x = Util.GetXForPosition(v);
                    if (x > posX && x < 8 && Util.GetYForPosition(v) >= 0)
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
        public static ulong PseudomoveBitboard(int piece)
        {
            ulong result = 0;
            int position = Util.GetPieceOffset(piece);
            int posX = Util.GetXForPosition(position);
            int posY = Util.GetYForPosition(position);
            int x, y, v;
            ulong one = 1;
            //expand out from piece position

            for (int i = 1; i < 8; i++)
            {
                v = (position + (9 * i));//up right
                x = Util.GetXForPosition(v);
                y = Util.GetYForPosition(v);
                //prevent wrapping
                if (x > posX && x < 8 && y < 8)
                {
                    result |= (one << (v));
                }

                v = (position - (9 * i));//down left
                x = Util.GetXForPosition(v);
                y = Util.GetYForPosition(v);
                if (x < posX && x >= 0 && y >= 0)
                {
                    result |= (one << (v));
                }

                v = (position + (7 * i));//up left
                x = Util.GetXForPosition(v);
                y = Util.GetYForPosition(v);
                if (x < posX && x >= 0 && y < 8)
                {
                    result |= (one << (v));
                }

                v = (position - (7 * i));//down right
                x = Util.GetXForPosition(v);
                y = Util.GetYForPosition(v);
                if (x > posX && x < 8 && y >= 0)
                {
                    result |= (one << (v));
                }
            }
            return result;
        }
    }
}
