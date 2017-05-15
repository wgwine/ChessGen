using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{

    public interface IMoveGenerator
    {
        List<int> PossiblePositions(int piece);
    }
    public static class PawnMoveGenerator
    {
        public static List<int> PossiblePositions(int piece, int[] board)
        {
            bool isWhite = Util.IsWhite(piece);
            List<int> tempResult = new List<int>();
            List<int> result = new List<int>();
            int position = Util.GetPieceOffset(piece);
            int depositionedPiece = Util.DepositionPiece(piece);
            int positionX = Util.GetXForPosition(position);
            int positionY = Util.GetYForPosition(position);
            int standardMoveOffset = isWhite ? 8 : -8;
            int captureOffset1 = isWhite ? 7 : -7;
            int captureOffset2 = isWhite ? 9 : -9;
            //if white
            if (isWhite)
            {
                if (positionY < 6)
                {
                    if (board[position + standardMoveOffset] == 0)
                    {
                        tempResult.Add((position + standardMoveOffset));
                    }
                    //captures
                    if (positionX > 0 && board[position + captureOffset1] > 0 && Util.IsWhite(board[position + captureOffset1]) != isWhite)
                    {
                        tempResult.Add((position + captureOffset1));
                    }

                    if (positionX < 7 && board[position + captureOffset2] > 0 && Util.IsWhite(board[position + captureOffset2]) != isWhite)
                    {
                        tempResult.Add((position + captureOffset2));
                    }


                    if (positionY == 1)
                    {
                        if (board[position + standardMoveOffset] == 0 && board[position + (2 * standardMoveOffset)] == 0)
                        {
                            tempResult.Add((position + (2 * standardMoveOffset)));
                        }
                    }
                }
                else if(positionY == 6)
                {
                    //pawn promotion
                    if (board[position + standardMoveOffset] == 0)
                        result.Add((PieceTypeFENMap.PieceValue('Q') + position + standardMoveOffset));

                    if (positionX > 0 && board[position + captureOffset1] > 0 && Util.IsWhite(board[position + captureOffset1]) != isWhite)
                        result.Add((PieceTypeFENMap.PieceValue('Q') + position + captureOffset1));

                    if (positionX < 7 && board[position + captureOffset2] > 0 && Util.IsWhite(board[position + captureOffset2]) != isWhite)
                        result.Add((PieceTypeFENMap.PieceValue('Q') + position + captureOffset2));
                }
            }
            else
            {
                if (positionY > 1)
                {
                    if (board[position + standardMoveOffset] == 0)
                        tempResult.Add((position + standardMoveOffset));

                    if (positionX <7 && board[position + captureOffset1] > 0 && Util.IsWhite(board[position + captureOffset1]) != isWhite)
                        tempResult.Add((position + captureOffset1));

                    if (positionX > 0 && board[position + captureOffset2] > 0 && Util.IsWhite(board[position + captureOffset2]) != isWhite)
                        tempResult.Add((position + captureOffset2));

                    if (positionY == 6)
                    {
                        if (board[position + standardMoveOffset] == 0 && board[position + (2 * standardMoveOffset)] == 0)
                            tempResult.Add((position + (2 * standardMoveOffset)));
                    }
                }
                else if (positionY == 1)
                {
                    //pawn promotion
                    if (board[position + standardMoveOffset] == 0)
                        result.Add((PieceTypeFENMap.PieceValue('q') + position + standardMoveOffset));

                    if (positionX < 7 && board[position + captureOffset1] > 0 && Util.IsWhite(board[position + captureOffset1]) != isWhite)
                        result.Add((PieceTypeFENMap.PieceValue('q') + position + captureOffset1));

                    if (positionX > 0 && board[position + captureOffset2] > 0 && Util.IsWhite(board[position + captureOffset2]) != isWhite)
                        result.Add((PieceTypeFENMap.PieceValue('q') + position + captureOffset2));
                }
            }


            foreach (int newPosition in tempResult)
            {
                result.Add(newPosition | depositionedPiece);
            }

            return result;
        }
    }
}
