using System.Collections.Generic;


namespace Chess.Models
{
    public static class KingMoveGenerator
    {
        public static List<short> PossiblePositions(short piece, List<short> board)
        {
            bool isWhite = Util.IsWhite(piece);
            List<short> tempResult = new List<short>();
            List<short> result = new List<short>();
            short depositionedPiece = Util.DepositionPiece(piece);
            short position = Util.GetPieceOffset(piece);
            short positionX = Util.GetXForPosition(position);
            short positionY = Util.GetYForPosition(position);

            if (positionX < 7)
            {
                //right
                tempResult.Add((short)(position + 1));

                if (positionY < 7)
                {
                    //up right
                    tempResult.Add((short)(position + 9));
                }
                if (positionY > 0)
                {
                    //down right
                    tempResult.Add((short)(position - 7));
                }
            }

            if (positionX > 0)
            {
                //left
                tempResult.Add((short)(position - 1));

                if (positionY < 7)
                {
                    //up left
                    tempResult.Add((short)(position + 7));
                }
                if (positionY > 0)
                {
                    //down left
                    tempResult.Add((short)(position - 9));
                }
            }

            if (positionY <7)
            {
                //up
                tempResult.Add((short)(position + 8));
            }

            if (positionY > 0)
            {
                //down
                tempResult.Add((short)(position - 8));
            }
            foreach(short move in tempResult)
            {
                short offset = Util.GetPieceOffset(move);
                if (board[move] == 0 || (board[offset] > 0 && Util.IsWhite(board[offset]) != isWhite))
                {
                    result.Add((short)(move| depositionedPiece));
                }
            }
            return result;
        }
    }
}
