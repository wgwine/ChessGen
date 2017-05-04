using System;
using System.Collections.Generic;

namespace Chess.Models
{
    public static class QueenMoveGenerator 
    {
        public static List<short> PossiblePositions(short piece)
        {
            List<short> result = new List<short>();
            short position = Util.GetPieceOffset(piece);
            short unpositionedPiece = (short)(piece - position);
            short posX = Util.GetXForPosition(position);

            for (byte i = 0; i < 8; i++)
            {
                short a = (short)(position + (9 * i));//up right
                short b = (short)(position - (9 * i));//down left
                short c = (short)(position + (7 * i));//up left
                short d = (short)(position - (7 * i));//down right

                if (Util.GetXForPosition(a) > posX && Util.GetXForPosition(a) < 8 && Util.GetYForPosition(a) < 8)
                    result.Add((short)(unpositionedPiece + a));

                if (Util.GetXForPosition(b) < posX && Util.GetXForPosition(b) >= 0 && Util.GetYForPosition(b) >= 0)
                    result.Add((short)(unpositionedPiece + b));

                if (Util.GetXForPosition(c) < posX && Util.GetXForPosition(c) >= 0 && Util.GetYForPosition(c) < 8)
                    result.Add((short)(unpositionedPiece + c));

                if (Util.GetXForPosition(d) > posX && Util.GetXForPosition(d) < 8 && Util.GetYForPosition(d) >= 0)
                    result.Add((short)(unpositionedPiece + d));
            }

            byte R = (byte)(Util.GetYForPosition(position) * 8);
            for (byte i = R; i < R + 7; i++)
            {
                if (i != position)
                {
                    result.Add((short)(unpositionedPiece + i + R));
                }
            }
            R = (byte)Util.GetXForPosition(position);
            for (byte i = R; i <= (56 + R); i += 8)
            {
                result.Add((short)(unpositionedPiece + i + R));
            }
            return result;
        }
    }
}
