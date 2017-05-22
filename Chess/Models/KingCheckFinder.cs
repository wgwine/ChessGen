using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public static class KingCheckFinder
    {
        public static bool IsKingChecked(int currentKingPosition, List<Move> enemyMoves)
        {
            foreach (Move m in enemyMoves)
            {
                if (Util.GetPieceOffset(m.To) == currentKingPosition)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
