using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public class Move
    {
        public int From;
        public int To;
        public int? Captured;
        public double MaterialScore;
        public bool removesOOO;
        public bool removesOO;
        public int CastlePiece;
        public Move()
        {
        }
        public Move(int from, int to)
        {
            From = from;
            To = to;
        }
        public Move(int from, int to, int captured)
        {
            From = from;
            To = to;
            Captured = captured;
        }
    }
}
