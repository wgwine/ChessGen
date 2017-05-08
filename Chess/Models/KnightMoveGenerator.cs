using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public static class KnightMoveGenerator
    {

        public static List<short> PossiblePositions(short piece, List<short> board)
        {
            bool isWhite = Util.IsWhite(piece);
            List<short> result = new List<short>();
            List<short> tempResult = new List<short>();
            short position = Util.GetPieceOffset(piece);
            short positionX = Util.GetXForPosition(position);
            short positionY = Util.GetYForPosition(position);

            if (positionX < 7)
            {
                if (positionY < 6)
                {
                    //1:00
                    tempResult.Add((short)(piece + 17));
                }
                if (positionY > 1)
                {
                    //5:00
                    tempResult.Add((short)(piece - 15));
                }

            }
            if (positionX < 6)
            {
                if (positionY < 7)
                {
                    //2:00
                    tempResult.Add((short)(piece +10));
                }
                if (positionY > 0)
                {
                    //4:00
                    tempResult.Add((short)(piece - 6));
                }

            }

            if (positionX > 0)
            {
                if (positionY > 1)
                {
                    //7:00
                    tempResult.Add((short)(piece - 17));
                }
                if (positionY < 6)
                {
                    //11:00
                    tempResult.Add((short)(piece + 15));
                }
            }
            if (positionX > 1)
            {
                if (positionY > 0)
                {
                    //8:00
                    tempResult.Add((short)(piece - 10));
                }
                if (positionY < 7)
                {
                    //10:00
                    tempResult.Add((short)(piece + 6));
                }
            }
            foreach (short move in tempResult)
            {
                short offset = Util.GetPieceOffset(move);
                if (board[offset] > 0 && Util.IsWhite(board[offset]) != isWhite)
                {
                    result.Add(move);
                }
            }
            return result;
        }
    }
}
