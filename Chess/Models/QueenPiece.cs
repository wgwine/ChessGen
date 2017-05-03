using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public class QueenPiece : GamePiece
    {
        List<byte> m_PossibleNextMoves;
        public override List<byte> GetPossibleNextMoves()
        {
            for (byte i = 0; i < 8; i++)
            {
                byte a = (byte)(Position + (9 * i));//up right
                byte b = (byte)(Position - (9 * i));//down left
                byte c = (byte)(Position + (7 * i));//up left
                byte d = (byte)(Position - (7 * i));//down right

                if (Util.GetXForPosition(a) > Util.GetXForPosition(Position) && Util.GetXForPosition(a) < 8 && Util.GetYForPosition(a) < 8)
                    m_PossibleNextMoves.Add(a);

                if (Util.GetXForPosition(b) < Util.GetXForPosition(Position) && Util.GetXForPosition(b) >= 0 && Util.GetYForPosition(b) >= 0)
                    m_PossibleNextMoves.Add(b);

                if (Util.GetXForPosition(c) < Util.GetXForPosition(Position) && Util.GetXForPosition(c) >= 0 && Util.GetYForPosition(c) < 8)
                    m_PossibleNextMoves.Add(c);

                if (Util.GetXForPosition(d) > Util.GetXForPosition(Position) && Util.GetXForPosition(d) < 8 && Util.GetYForPosition(d) >= 0)
                    m_PossibleNextMoves.Add(d);
            }

            byte R = (byte)(Util.GetYForPosition(Position) * 8);
            for (byte i = R; i < R + 7; i++)
            {
                if (i != Position)
                {
                    m_PossibleNextMoves.Add(i);
                }
            }
            R = Util.GetXForPosition(Position);
            for (byte i = R; i <= (56 + R); i += 8)
            {
                m_PossibleNextMoves.Add(i);
            }
            return m_PossibleNextMoves;
        }
    }
}
