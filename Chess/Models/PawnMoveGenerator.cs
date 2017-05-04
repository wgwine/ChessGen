using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{

    public interface IMoveGenerator
    {
        List<short> PossiblePositions(short piece);
    }
    public static class PawnMoveGenerator
    {
        public static List<short> PossiblePositions(short piece, List<short> board)
        {
            List<short> result = new List<short>();
            short position = Util.GetPieceOffset(piece);
            short positionX = Util.GetXForPosition(position);
            short positionY = Util.GetYForPosition(position);
            //if white
            if (Util.GetBitAtPosition(piece, 9))
            {
                result.Add((short)(piece + 8));
                result.Add((short)(piece + 7));
                result.Add((short)(piece + 9));
                if (positionY == 1)
                {
                    result.Add((short)(piece + 16));
                }
            }
            else
            {
                result.Add((short)(piece - 8));
                result.Add((short)(piece - 7));
                result.Add((short)(piece - 9));
                if (positionY == 6)
                {
                    result.Add((short)(piece - 16));
                }
            }
            return result;
        }
    }
}
