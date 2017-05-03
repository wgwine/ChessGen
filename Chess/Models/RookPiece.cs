using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public class RookPiece : GamePiece
    {
        List<byte> m_PossibleNextMoves;
        public override List<byte> GetPossibleNextMoves()
        {
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
