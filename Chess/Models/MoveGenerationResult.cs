using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public enum EndgameType
    {
        None,
        Stalemate,
        Checkmate
    }
    public class MoveGenerationResult
    {
        public List<Move> Moves;
        public EndgameType Endgame;
        public MoveGenerationResult()
        {
            Moves = new List<Move>();
            Endgame = EndgameType.None;
        }
    }
}
