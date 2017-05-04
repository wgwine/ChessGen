using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public static class RookMoveGenerator
    {

        public static List<short> PossiblePositions(short piece, List<short> board)
        {
            List<short> result = new List<short>();
            short position = Util.GetPieceOffset(piece);
            short unpositionedPiece = (short)(piece - position);
            short R = (short)(Util.GetYForPosition(position) * 8);
            for (short i = R; i < R + 7; i++)
            {
                if (i != position)
                {
                    result.Add((short)(unpositionedPiece+i+R));
                }
            }
            R = (short)Util.GetXForPosition(position);
            for (short i = R; i <= (56 + R); i += 8)
            {
                result.Add((short)(unpositionedPiece + i));
            }
            return result;
        }
    }
}
