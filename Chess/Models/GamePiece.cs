using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public abstract class GamePiece
    {
        short m_Piece;
        PieceType m_pieceType;
        short m_position;
        bool m_isWhite;
        public GamePiece()
        {
        }
        public GamePiece(short piece)
        {
            m_Piece = piece;
            m_pieceType = Util.GetPieceType(piece);
            m_position = Util.GetPieceOffset(piece);
            m_isWhite = Util.GetBitAtPosition(piece, 9);
        }
        public short Position
        {
            get
            {
                return m_position;
            }
        }
        public bool IsWhite
        {
            get
            {
                return m_isWhite;
            }
        }
        public PieceType PieceType
        {
            get
            {
                return m_pieceType;
            }
        }
        public byte Y
        {
            get
            {
                return (byte)(Position >> 3);
            }
        }
        public byte X
        {
            get
            {
                return (byte)(Position & 7);
            }
        }


        public abstract List<byte> GetPossibleNextMoves();
    }
}
