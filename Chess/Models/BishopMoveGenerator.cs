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
            List<short> result = new List<short>();
            short position = Util.GetPieceOffset(piece);
            short unpositionedPiece = (short)(piece - position);
            short posX = Util.GetXForPosition(position);
            for (short i = 0; i < 8; i++)
            {
                short a = (short)(position + (9 * i));//up right
                short b = (short)(position - (9 * i));//down left
                short c = (short)(position + (7 * i));//up left
                short d = (short)(position - (7 * i));//down right

                if (Util.GetXForPosition(a) > posX && Util.GetXForPosition(a) < 8 && Util.GetYForPosition(a) < 8)
                    result.Add((short)(unpositionedPiece+a));

                if (Util.GetXForPosition(b) < posX && Util.GetXForPosition(b) >= 0 && Util.GetYForPosition(b) >= 0)
                    result.Add((short)(unpositionedPiece + b));

                if (Util.GetXForPosition(c) < posX && Util.GetXForPosition(c) >= 0 && Util.GetYForPosition(c) < 8)
                    result.Add((short)(unpositionedPiece + c));

                if (Util.GetXForPosition(d) > posX && Util.GetXForPosition(d) < 8 && Util.GetYForPosition(d) >= 0)
                    result.Add((short)(unpositionedPiece + d));
            }
            return result;
        }
    }
}
