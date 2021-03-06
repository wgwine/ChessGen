﻿using System;
using System.Collections.Generic;


namespace Chess.Models
{
    public static class KingMoveGenerator
    {
        public static ulong PseudomoveBitboard(int piece)
        {
            int position = Util.GetPieceOffset(piece);
            int positionX = Util.GetXForPosition(position);
            int positionY = Util.GetYForPosition(position);
            ulong resultBoard = 0;
            ulong one = 1;
            if (positionX < 7)
            {
                //right
                resultBoard |= (one << (position + 1));

                if (positionY < 7)
                {
                    //up right
                    resultBoard |= (one << (position + 9));
                }
                if (positionY > 0)
                {
                    //down right
                    resultBoard |= (one << (position - 7));
                }
            }

            if (positionX > 0)
            {
                //left
                resultBoard |= (one << (position - 1));

                if (positionY < 7)
                {
                    //up left
                    resultBoard |= (one << (position + 7));
                }
                if (positionY > 0)
                {
                    //down left
                    resultBoard |= (one << (position - 9));
                }
            }

            if (positionY < 7)
            {
                //up
                resultBoard |= (one << (position + 8));
            }

            if (positionY > 0)
            {
                //down
                resultBoard |= (one << (position - 8));
            }

            return resultBoard;
        }
        public static List<int> PossiblePositions(int piece, int[] board)
        {
            bool isWhite = Util.IsWhite(piece);
            List<int> tempResult = new List<int>();
            List<int> result = new List<int>();

            int position = Util.GetPieceOffset(piece);
            int depositionedPiece =(piece - position);
            int positionX = Util.GetXForPosition(position);
            int positionY = Util.GetYForPosition(position);

            if (positionX < 7)
            {
                //right
                tempResult.Add(position + 1);

                if (positionY < 7)
                {
                    //up right
                    tempResult.Add(position + 9);
                }
                if (positionY > 0)
                {
                    //down right
                    tempResult.Add(position - 7);
                }
            }

            if (positionX > 0)
            {
                //left
                tempResult.Add(position - 1);

                if (positionY < 7)
                {
                    //up left
                    tempResult.Add(position + 7);
                }
                if (positionY > 0)
                {
                    //down left
                    tempResult.Add(position - 9);
                }
            }

            if (positionY <7)
            {
                //up
                tempResult.Add(position + 8);
            }

            if (positionY > 0)
            {
                //down
                tempResult.Add(position - 8);
            }
            foreach(int move in tempResult)
            {
                if (board[move] == 0 || (board[move] > 0 && Util.IsWhite(board[move]) != isWhite))
                {
                    result.Add((move + depositionedPiece));
                }
            }
            return result;
        }
        public static ulong PossiblePositionsBitboard(int piece, int[] board)
        {
            bool isWhite = Util.IsWhite(piece);
            List<int> tempResult = new List<int>();
            ulong result = 0;
            ulong one = 1;
            int position = Util.GetPieceOffset(piece);
            int positionX = Util.GetXForPosition(position);
            int positionY = Util.GetYForPosition(position);

            if (positionX < 7)
            {
                //right
                tempResult.Add(position + 1);

                if (positionY < 7)
                {
                    //up right
                    tempResult.Add(position + 9);
                }
                if (positionY > 0)
                {
                    //down right
                    tempResult.Add(position - 7);
                }
            }

            if (positionX > 0)
            {
                //left
                tempResult.Add(position - 1);

                if (positionY < 7)
                {
                    //up left
                    tempResult.Add(position + 7);
                }
                if (positionY > 0)
                {
                    //down left
                    tempResult.Add(position - 9);
                }
            }

            if (positionY < 7)
            {
                //up
                tempResult.Add(position + 8);
            }

            if (positionY > 0)
            {
                //down
                tempResult.Add(position - 8);
            }
            foreach (int move in tempResult)
            {
                if (board[move] == 0 || (board[move] > 0 && Util.IsWhite(board[move]) != isWhite))
                {
                    result |= (one << (move));
                }
            }
            return result;
        }

    }
}
