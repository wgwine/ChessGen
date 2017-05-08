using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public static class KingCheckFinder
    {
        public static bool IsKingChecked(short piece, List<short> board, List<Move> moves)
        {



            return false;
        }
        public static List<short> FindCheckLocks(short piece, List<short> board, List<Move> moves) 
        {

            List<short> result = new List<short>();
            int colorMatch = Util.IsWhite(piece) ? 1 : 2;
            bool isWhite = Util.IsWhite(piece);
            short position = Util.GetPieceOffset(piece);
            short unpositionedPiece = Util.DepositionPiece(piece);
            short kingX = Util.GetXForPosition(position);
            short kingY = Util.GetXForPosition(position);
            Dictionary<string, List<short>> myDiagonals = new Dictionary<string, List<short>>() {
                { "UR", new List<short>() },
                { "DL", new List<short>() },
                { "UL", new List<short>() },
                { "DR", new List<short>() }
            };
            Dictionary<string, List<short>> checkBlockableSquares = new Dictionary<string, List<short>>() {
                { "UR", new List<short>() },
                { "DL", new List<short>() },
                { "UL", new List<short>() },
                { "DR", new List<short>() }
            };
            bool isInCheck = false;
            bool stopA = false, stopB = false, stopC = false, stopD = false;
            for (short i = 1; i < 8; i++)
            {

                short a = (short)(position + (9 * i));//up right
                short b = (short)(position - (9 * i));//down left
                short c = (short)(position + (7 * i));//up left
                short d = (short)(position - (7 * i));//down right
                //prevent wrapping

                short x = Util.GetXForPosition(a);
                short y = Util.GetYForPosition(a);
                if (x > kingX && x < 8 && y < 8 && !stopA)
                {
                    var newPos = (short)(unpositionedPiece + a);
                    checkBlockableSquares["UR"].Add(newPos);
                    //if there is a friendly piece on this diagonal
                    if (board[a] > 0 && Util.IsWhite(board[a]) == isWhite)
                    {
                        //if there is more than 1 friendly on diagonal, neither of them is check locked, stop checking this diagonal
                        if (myDiagonals["UR"].Count() > 0)
                        {
                            stopA = true;
                            //no pieces on this diagonal are check locked
                            myDiagonals["UR"].Clear();
                        }
                        else
                        {
                            myDiagonals["UR"].Add(board[a]);
                        }
                    }
                    else if(board[a] > 0) //enemy piece detected
                    {
                        var enemyType = Util.GetPieceType(board[a]);
                        //diagonal attackers
                        if (enemyType == PieceType.Bishop || enemyType == PieceType.Queen ||(i==1 && enemyType==PieceType.Pawn))
                        {
                            //moving the previously detected friendly piece would cause check
                            if (myDiagonals["UR"].Count() > 0)
                            {
                                result.Add(myDiagonals["UR"].First());
                                stopA = true;
                                myDiagonals["UR"].Clear();
                            }
                            else
                            {
                                //we are in check now.
                                myDiagonals["UR"].Add(board[a]);
                            }
                        }

                    }
                }

                if (Util.GetXForPosition(b) < kingX && Util.GetXForPosition(b) >= 0 && Util.GetYForPosition(b) >= 0)
                {
                    var newPos = (short)(unpositionedPiece + b);
                    if (board[b] > 0)
                    {
                        if (board[b] != colorMatch)
                            result.Add((short)(unpositionedPiece + b));
                    }
                    else
                    {
                        result.Add((short)(unpositionedPiece + b));
                    }
                }

                if (Util.GetXForPosition(c) < kingX && Util.GetXForPosition(c) >= 0 && Util.GetYForPosition(c) < 8)
                {
                    var newPos = (short)(unpositionedPiece + c);
                    if (board[c] > 0)
                    {
                        if (board[c] != colorMatch)
                            result.Add((short)(unpositionedPiece + c));
                    }
                    else
                    {
                        result.Add((short)(unpositionedPiece + c));
                    }
                }

                if (Util.GetXForPosition(d) > kingX && Util.GetXForPosition(d) < 8 && Util.GetYForPosition(d) >= 0)
                {
                    var newPos = (short)(unpositionedPiece + d);
                    if (board[d] > 0)
                    {
                        if (board[d] != colorMatch)
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
