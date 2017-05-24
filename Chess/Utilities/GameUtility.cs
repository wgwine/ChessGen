using Chess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Utilities
{
    public static class GameUtility
    {
        public static string GameToFEN(Game g)
        {
            string[] resultArr = new string[8 * 8];
            foreach (int piece in g.Board.Where(e => e > 0))
            {
                resultArr[Util.GetPieceOffset(piece)] = PieceTypeFENMap.PieceName(piece).ToString();
            }
            int count = 0;
            StringBuilder sb = new StringBuilder();
            StringBuilder sbRow = new StringBuilder();
            //we have to re-reverse the result for text-box
            int skipGrabber = 0;
            foreach (string p in resultArr.Reverse())
            {
                if (count % 8 == 0 && count > 0)
                {
                    sb.Append("/");
                }
                if (p != null)
                {
                    if (skipGrabber != 0)
                    {
                        sbRow.Append(skipGrabber);
                        skipGrabber = 0;
                    }
                    sbRow.Append(p);
                }
                else
                {
                    skipGrabber++;
                }

                count++;
                if (count % 8 == 0)
                {
                    if (skipGrabber != 0)
                    {
                        sbRow.Append(skipGrabber);
                        skipGrabber = 0;
                    }
                }
                if (count % 8 == 0)
                {
                    string ss = new string(sbRow.ToString().Reverse().ToArray());
                    sb.Append(ss);
                    sbRow.Clear();
                }
            }

            //which player has next move
            sb.Append(" ");
            if (g.WhiteToMove)
            {
                sb.Append("w");
            }
            else
            {
                sb.Append("b");
            }

            //castling
            sb.Append(" ");
            if (g.WhiteOOCastle || g.WhiteOOOCastle || g.BlackOOCastle || g.BlackOOOCastle)
            {
                if (g.WhiteOOCastle)
                {
                    sb.Append("K");
                }
                if (g.WhiteOOOCastle)
                {
                    sb.Append("Q");
                }
                if (g.BlackOOCastle)
                {
                    sb.Append("k");
                }
                if (g.BlackOOOCastle)
                {
                    sb.Append("q");
                }
            }
            else
            {
                sb.Append("-");
            }


            if (g.EnPassantSquare.HasValue)
            {
                sb.Append(" ");
                sb.Append(Util.IntToFile(Util.GetXForPosition((byte)g.EnPassantSquare.Value)));
                sb.Append(Util.GetYForPosition((byte)g.EnPassantSquare.Value) + 1);
            }
            else
            {
                sb.Append(" -");
            }
            return sb.ToString();
        }
        public static string GameToString(Game g)
        {
            char[][] resultArr = new char[8][];
            for (int i = 0; i < 8; i++)
            {
                resultArr[i] = new char[8];
            }

            int rowCount = 0;
            int rowIndex = 0;
            foreach (int piece in g.Board.Where(e => e > 0))
            {


                resultArr[Util.GetYForPiece(piece)][Util.GetXForPiece(piece)] = PieceTypeFENMap.PieceName(piece);
                rowCount++;
                if (rowCount % 8 == 0)
                {
                    rowIndex++;
                }

            }
            int count = 0;
            StringBuilder sb = new StringBuilder();
            //we have to re-reverse the result for text-box
            char[] rowText = new char[8];
            resultArr = resultArr.Reverse().ToArray();
            for (int i = 0; i < 8; i++)
            {
                foreach (char p in resultArr[i])
                {
                    rowText[count % 8] = p == 0 ? '-' : p;
                    count++;
                    if (count % 8 == 0)
                    {
                        sb.Append(rowText);
                        sb.Append("\r\n");
                    }
                }
            }
            return sb.ToString();
        }
    }
}
