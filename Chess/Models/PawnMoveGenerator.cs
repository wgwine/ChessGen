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
        //generate bitboard of moves
        public static ulong PseudomoveBitboard(int piece, int[] board)
        {
            bool isWhite = Util.IsWhite(piece);
            int position = Util.GetPieceOffset(piece);
            int positionX = Util.GetXForPosition(position);
            int positionY = Util.GetYForPosition(position);
            ulong result = 0;
            ulong one = 1;

            if (isWhite)
            {
                if (positionY < 6)
                {
                    if (board[position + 8] == 0)
                    {
                        result |= (one << (position + 8));
                    }

                    //captures
                    if (positionX > 0)
                    {
                        result |= (one << (position + 7));
                    }

                    if (positionX < 7)
                    {
                        result |= (one << (position + 9));
                    }
                    //initial double move 
                    if (positionY == 1)
                    {
                        result |= (one << (position + (2 * 8)));
                    }
                }
                else if (positionY == 6)
                {
                    //pawn promotion
                    result |= QueenMoveGenerator.PseudomoveBitboard((PieceTypeFENMap.PieceValue('Q') + position + 8));

                    if (positionX > 0)
                        result |= QueenMoveGenerator.PseudomoveBitboard((PieceTypeFENMap.PieceValue('Q') + position + 7));

                    if (positionX < 7)
                        result |= QueenMoveGenerator.PseudomoveBitboard((PieceTypeFENMap.PieceValue('Q') + position + 9));
                }
            }
            else
            {
                if (positionY > 1)
                {
                    if (board[position - 8] == 0)
                        result |= (one << (position - 8));

                    //captures
                    if (positionX < 7)
                        result |= (one << (position - 7));

                    if (positionX > 0)
                        result |= (one << (position - 9));

                    //initial double move 
                    if (positionY == 6)
                    {
                        result |= (one << (position - (2 * 8)));
                    }
                }
                else if (positionY == 1)
                {
                    //pawn promotion
                    result |= QueenMoveGenerator.PseudomoveBitboard((PieceTypeFENMap.PieceValue('q') + position - 8));

                    if (positionX < 7)
                        result |= QueenMoveGenerator.PseudomoveBitboard((PieceTypeFENMap.PieceValue('q') + position - 7));

                    if (positionX > 0)
                        result |= QueenMoveGenerator.PseudomoveBitboard((PieceTypeFENMap.PieceValue('q') + position - 9));
                }
            }

            return result;
        }
        //generate real moves based on board state
        public static List<int> PossiblePositions(int piece, int[] board)
        {
            bool isWhite = Util.IsWhite(piece);
            List<int> tempResult = new List<int>();
            List<int> result = new List<int>();
            int position = Util.GetPieceOffset(piece);
            int depositionedPiece = Util.DepositionPiece(piece);
            int positionX = Util.GetXForPosition(position);
            int positionY = Util.GetYForPosition(position);
            if (isWhite)
            {
                if (positionY < 6)
                {
                    if (board[position + 8] == 0)
                    {
                        result.Add((piece + 8));
                    }
                    //captures
                    if (positionX > 0 && board[position + 7] > 0 && Util.IsWhite(board[position + 7]) != isWhite)
                    {
                        result.Add((piece + 7));
                    }

                    if (positionX < 7 && board[position + 9] > 0 && Util.IsWhite(board[position + 9]) != isWhite)
                    {
                        result.Add((piece + 9));
                    }


                    if (positionY == 1)
                    {
                        if (board[position + 8] == 0 && board[position + (2 * 8)] == 0)
                        {
                            result.Add((piece + (2 * 8)));
                        }
                    }
                }
                else if (positionY == 6)
                {
                    //pawn promotion
                    if (board[position + 8] == 0)
                        result.Add((PieceTypeFENMap.PieceValue('Q') + position + 8));

                    if (positionX > 0 && board[position + 7] > 0 && Util.IsWhite(board[position + 7]) != isWhite)
                        result.Add((PieceTypeFENMap.PieceValue('Q') + position + 7));

                    if (positionX < 7 && board[position + 7] > 0 && Util.IsWhite(board[position + 9]) != isWhite)
                        result.Add((PieceTypeFENMap.PieceValue('Q') + position + 9));
                }
            }
            else
            {
                if (positionY > 1)
                {
                    if (board[position - 8] == 0)
                        result.Add((piece - 8));

                    if (positionX < 7 && board[position - 7] > 0 && Util.IsWhite(board[position - 7]) != isWhite)
                        result.Add((piece - 7));

                    if (positionX > 0 && board[position - 9] > 0 && Util.IsWhite(board[position - 9]) != isWhite)
                        result.Add((piece - 9));

                    if (positionY == 6)
                    {
                        if (board[position - 8] == 0 && board[position - (2 * 8)] == 0)
                            result.Add((piece - (2 * 8)));
                    }
                }
                else if (positionY == 1)
                {
                    //pawn promotion
                    if (board[position - 8] == 0)
                        result.Add((PieceTypeFENMap.PieceValue('q') + position - 8));

                    if (positionX < 7 && board[position - 7] > 0 && Util.IsWhite(board[position - 7]) != isWhite)
                        result.Add((PieceTypeFENMap.PieceValue('q') + position - 7));

                    if (positionX > 0 && board[position - 9] > 0 && Util.IsWhite(board[position - 9]) != isWhite)
                        result.Add((PieceTypeFENMap.PieceValue('q') + position - 9));
                }
            }
            return result;
        }
        public static ulong PossiblePositionsBitboard(int piece, int[] board)
        {
            bool isWhite = Util.IsWhite(piece);
            ulong result = 0;
            ulong one = 1;
            int position = Util.GetPieceOffset(piece);
            int positionX = Util.GetXForPosition(position);
            int positionY = Util.GetYForPosition(position);
            if (isWhite)
            {
                if (positionY < 7)
                {
                    if (board[position + 8] == 0)
                    {
                        result |= (one << (position + 8));
                    }
                    //captures
                    if (positionX > 0 && board[position + 7] > 0 && Util.IsWhite(board[position + 7]) != isWhite)
                    {
                        result |= (one << (position + 7));
                    }

                    if (positionX < 7 && board[position + 9] > 0 && Util.IsWhite(board[position + 9]) != isWhite)
                    {
                        result |= (one << (position + 9));
                    }


                    if (positionY == 1)
                    {
                        if (board[position + 8] == 0 && board[position + (2 * 8)] == 0)
                        {
                            result |= (one << (position + (2 * 8)));
                        }
                    }
                }
            }
            else
            {
                if (positionY > 0)
                {
                    if (board[position - 8] == 0)
                        result |= (one << (position - 8));

                    if (positionX < 7 && board[position - 7] > 0 && Util.IsWhite(board[position - 7]) != isWhite)
                        result |= (one << (position - 7));

                    if (positionX > 0 && board[position - 9] > 0 && Util.IsWhite(board[position - 9]) != isWhite)
                        result |= (one << (position - 9));

                    if (positionY == 6)
                    {
                        if (board[position - 8] == 0 && board[position - (2 * 8)] == 0)
                            result |= (one << (position - (2 * 8)));
                    }
                }
            }
            return result;
        }
    }
}
