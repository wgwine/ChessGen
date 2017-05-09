using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{

    public interface IMoveGenerator
    {
        List<short> PossiblePositions(short piece);
    }
    public static class PawnMoveGenerator
    {
        public static List<short> PossiblePositions(short piece, List<short> board)
        {
            bool isWhite = Util.IsWhite(piece);
            int colorMatch = isWhite ? 1 : 2;
            List<short> tempResult = new List<short>();
            List<short> result = new List<short>();
            short position = Util.GetPieceOffset(piece);
            short depositionedPiece = Util.DepositionPiece(piece);
            short positionX = Util.GetXForPosition(position);
            short positionY = Util.GetYForPosition(position);
            short standardMoveOffset = isWhite ? (short)8 : (short)-8;
            short captureOffset1 = isWhite ? (short)7 : (short)-7;
            short captureOffset2 = isWhite ? (short)9 : (short)-9;
            short opponentKing = (short)(isWhite ? 4 : 3);
            //if white
            if (isWhite)
            {
                if (positionY < 6)
                {
                    if (board[position + standardMoveOffset] == 0)
                    {
                        tempResult.Add((short)(position + standardMoveOffset));
                    }
                    //captures
                    if (board[position + captureOffset1] > 0 && Util.IsWhite(board[position + captureOffset1]) != isWhite)
                    {
                        tempResult.Add((short)(position + captureOffset1));
                    }

                    if (board[position + captureOffset2] > 0 && Util.IsWhite(board[position + captureOffset2]) != isWhite)
                    {
                        tempResult.Add((short)(position + captureOffset2));
                    }


                    if (positionY == 1)
                    {
                        if (board[position + standardMoveOffset] == 0 && board[position + (2 * standardMoveOffset)] == 0)
                        {
                            tempResult.Add((short)(position + (2 * standardMoveOffset)));
                        }
                    }
                }
                else if(positionY == 6)
                {
                    //pawn promotion
                    if (board[position + standardMoveOffset] == 0)
                        result.Add((short)(PieceTypeFENMap.PieceValue('Q') + position + standardMoveOffset));

                    if (positionX > 0 && board[position + captureOffset1] > 0 && Util.IsWhite(board[position + captureOffset1]) != isWhite)
                        result.Add((short)(PieceTypeFENMap.PieceValue('Q') + position + captureOffset1));

                    if (positionX < 7 && board[position + captureOffset2] > 0 && Util.IsWhite(board[position + captureOffset2]) != isWhite)
                        result.Add((short)(PieceTypeFENMap.PieceValue('Q') + position + captureOffset2));
                }
            }
            else
            {
                if (positionY > 1)
                {
                    if (board[position + standardMoveOffset] == 0)
                        tempResult.Add((short)(position + standardMoveOffset));

                    if (board[position + captureOffset1] > 0 && Util.IsWhite(board[position + captureOffset1]) != isWhite)
                        tempResult.Add((short)(position + captureOffset1));

                    if (board[position + captureOffset2] > 0 && Util.IsWhite(board[position + captureOffset2]) != isWhite)
                        tempResult.Add((short)(position + captureOffset2));

                    if (positionY == 6)
                    {
                        if (board[position + standardMoveOffset] == 0 && board[position + (2 * standardMoveOffset)] == 0)
                            tempResult.Add((short)(position + (2 * standardMoveOffset)));
                    }
                }
                else if (positionY == 1)
                {
                    //pawn promotion
                    if (board[position + standardMoveOffset] == 0)
                        result.Add((short)(PieceTypeFENMap.PieceValue('q') + position + standardMoveOffset));

                    if (positionX < 7 && board[position + captureOffset1] > 0 && Util.IsWhite(board[position + captureOffset1]) != isWhite)
                        result.Add((short)(PieceTypeFENMap.PieceValue('q') + position + captureOffset1));

                    if (positionX > 0 && board[position + captureOffset2] > 0 && Util.IsWhite(board[position + captureOffset2]) != isWhite)
                        result.Add((short)(PieceTypeFENMap.PieceValue('q') + position + captureOffset2));
                }
            }


            foreach (short newPosition in tempResult)
            {
                //short temp = newPosition;
                //add check value to moves, wont be needed if look for checks on turn
                //if(board[newPosition + captureOffset1] == opponentKing || board[newPosition + captureOffset2] == opponentKing)
                //{
                //    temp += 1024;
                //}

                result.Add((short)(newPosition + depositionedPiece));
            }

            return result;
        }
    }
}
