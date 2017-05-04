using System.Collections.Generic;


namespace Chess.Models
{
    public static class KingMoveGenerator
    {
        public static List<short> PossiblePositions(short piece)
        {
            List<short> result = new List<short>();
            short position = Util.GetPieceOffset(piece);
            short positionX = Util.GetXForPosition(position);
            short positionY = Util.GetYForPosition(position);

            if (positionX < 7)
            {
                //right
                result.Add((short)(piece + 1));

                if (positionY < 7)
                {
                    //up right
                    result.Add((short)(piece + 9));
                }
                if (positionY > 0)
                {
                    //down right
                    result.Add((short)(piece - 7));
                }
            }

            if (positionX > 0)
            {
                //left
                result.Add((short)(piece - 1));

                if (positionY < 7)
                {
                    //up left
                    result.Add((short)(piece +7));
                }
                if (positionY > 0)
                {
                    //down left
                    result.Add((short)(piece - 9));
                }
            }

            if (positionY <7)
            {
                //up
                result.Add((short)(piece +8));
            }

            if (positionY > 0)
            {
                //down
                result.Add((short)(piece - 8));
            }

            return result;
        }
    }
}
