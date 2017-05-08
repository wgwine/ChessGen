using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public class Move
    {
        public short From;
        public short To;
        public short? Captured;
        public Move()
        {
        }
        public Move(short from, short to)
        {
            From = from;
            To = to;
        }
        public Move(short from, short to, short captured)
        {
            From = from;
            To = to;
            Captured = captured;
        }
    }
}
