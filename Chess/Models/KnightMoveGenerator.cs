using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public static class KnightMoveGenerator
    {

        public static List<int> PossiblePositions(int piece, int[] board)
        {
            bool isWhite = Util.IsWhite(piece);
            List<int> result = new List<int>();
            List<int> tempResult = new List<int>();
            int position = Util.GetPieceOffset(piece);
            int positionX = Util.GetXForPosition(position);
            int positionY = Util.GetYForPosition(position);

            if (positionX < 7)
            {
                if (positionY < 6)
                {
                    //1:00
                    tempResult.Add((piece + 17));
                }
                if (positionY > 1)
                {
                    //5:00
                    tempResult.Add((piece - 15));
                }

            }
            if (positionX < 6)
            {
                if (positionY < 7)
                {
                    //2:00
                    tempResult.Add((piece +10));
                }
                if (positionY > 0)
                {
                    //4:00
                    tempResult.Add((piece - 6));
                }

            }

            if (positionX > 0)
            {
                if (positionY > 1)
                {
                    //7:00
                    tempResult.Add((piece - 17));
                }
                if (positionY < 6)
                {
                    //11:00
                    tempResult.Add((piece + 15));
                }
            }
            if (positionX > 1)
            {
                if (positionY > 0)
                {
                    //8:00
                    tempResult.Add((piece - 10));
                }
                if (positionY < 7)
                {
                    //10:00
                    tempResult.Add((piece + 6));
                }
            }
            foreach (int move in tempResult)
            {
                int offset = Util.GetPieceOffset(move);
                if (board[offset]==0 || (board[offset] > 0 && Util.IsWhite(board[offset]) != isWhite))
                {
                    result.Add(move);
                }
            }
            return result;
        }
    }
}
