using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public static class BishopMoveGenerator 
    {

        public static List<short> PossiblePositions(short piece, List<short> board)
        {
            bool isWhite = Util.IsWhite(piece);
            List<short> result = new List<short>();
            short position = Util.GetPieceOffset(piece);
            short unpositionedPiece = Util.DepositionPiece(piece);
            short posX = Util.GetXForPosition(position);
            bool stopA = false, stopB = false, stopC = false, stopD = false;
            for (short i = 1; i < 8; i++)
            {

                short a = (short)(position + (9 * i));//up right
                short b = (short)(position - (9 * i));//down left
                short c = (short)(position + (7 * i));//up left
                short d = (short)(position - (7 * i));//down right
                //prevent wrapping
                if (Util.GetXForPosition(a) > posX && Util.GetXForPosition(a) < 8 && Util.GetYForPosition(a) < 8 && !stopA)
                {
                    var newPos = (short)(unpositionedPiece + a);
                    if (board[a] > 0)
                    {
                        stopA = true;
                        if(Util.IsWhite(board[a]) != isWhite)
                            result.Add((short)(unpositionedPiece + a));
                    }else
                    {
                        result.Add((short)(unpositionedPiece + a));
                    }
                }

                if (Util.GetXForPosition(b) < posX && Util.GetXForPosition(b) >= 0 && Util.GetYForPosition(b) >= 0 && !stopB)
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

                if (Util.GetXForPosition(c) < posX && Util.GetXForPosition(c) >= 0 && Util.GetYForPosition(c) < 8 && !stopC)
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

                if (Util.GetXForPosition(d) > posX && Util.GetXForPosition(d) < 8 && Util.GetYForPosition(d) >= 0 && !stopD)
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
