using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public static class KingCheckFinder
    {
        public static bool IsKingChecked(int currentKingPosition, List<Move> enemyMoves)
        {
            foreach (Move m in enemyMoves)
            {
                if (Util.GetPieceOffset(m.To) == currentKingPosition)
                {
                    return true;
                }
            }

            return false;
        }
        public static List<int> FindCheckLocks(int currentKing, List<int> board, List<Move> moves) 
        {

            List<int> result = new List<int>();
            int colorMatch = Util.IsWhite(currentKing) ? 1 : 2;
            bool isWhite = Util.IsWhite(currentKing);
            int position = Util.GetPieceOffset(currentKing);
            int unpositionedPiece = Util.DepositionPiece(currentKing);
            int kingX = Util.GetXForPosition(position);
            int kingY = Util.GetXForPosition(position);
            Dictionary<string, List<int>> myDiagonals = new Dictionary<string, List<int>>() {
                { "UR", new List<int>() },
                { "DL", new List<int>() },
                { "UL", new List<int>() },
                { "DR", new List<int>() }
            };
            Dictionary<string, List<int>> checkBlockableSquares = new Dictionary<string, List<int>>() {
                { "UR", new List<int>() },
                { "DL", new List<int>() },
                { "UL", new List<int>() },
                { "DR", new List<int>() }
            };
            //bool isInCheck = IsKingChecked(currentKing, board,moves);
            bool stopA = false, stopB = false, stopC = false, stopD = false;
            for (int i = 1; i < 8; i++)
            {

                int a = (position + (9 * i));//up right
                int b = (position - (9 * i));//down left
                int c = (position + (7 * i));//up left
                int d = (position - (7 * i));//down right
                //prevent wrapping

                int x = Util.GetXForPosition(a);
                int y = Util.GetYForPosition(a);
                if (x > kingX && x < 8 && y < 8 && !stopA)
                {
                    var newPos = (unpositionedPiece + a);
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
                    var newPos = (unpositionedPiece + b);
                    if (board[b] > 0)
                    {
                        if (board[b] != colorMatch)
                            result.Add((unpositionedPiece + b));
                    }
                    else
                    {
                        result.Add((unpositionedPiece + b));
                    }
                }

                if (Util.GetXForPosition(c) < kingX && Util.GetXForPosition(c) >= 0 && Util.GetYForPosition(c) < 8)
                {
                    var newPos = (unpositionedPiece + c);
                    if (board[c] > 0)
                    {
                        if (board[c] != colorMatch)
                            result.Add((unpositionedPiece + c));
                    }
                    else
                    {
                        result.Add((unpositionedPiece + c));
                    }
                }

                if (Util.GetXForPosition(d) > kingX && Util.GetXForPosition(d) < 8 && Util.GetYForPosition(d) >= 0)
                {
                    var newPos = (unpositionedPiece + d);
                    if (board[d] > 0)
                    {
                        if (board[d] != colorMatch)
                            result.Add((unpositionedPiece + d));
                    }
                    else
                    {
                        result.Add((unpositionedPiece + d));
                    }
                }
            }

            return result;


        }
    }
}
