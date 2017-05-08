using System;
using System.Windows.Forms;
using System.Diagnostics;
using Chess.Models;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Chess
{
    public partial class Form1 : Form
    {
        GameService gs;
        int count = 0;
        public Form1()
        {
            InitializeComponent();
            gs = new GameService();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string value = textBox2.Text;
            Game g;
            int howmany = 100000;
            StringBuilder sb = new StringBuilder();
            Stopwatch w = new Stopwatch();
            w.Start();
            g = new Game(value);
            string s = "";
            count = 0;
            textBox3.Text = "";

            bool isMax = true;
            for (int jj = 0; jj < 100; jj++)
            {
                Move bestMove = MinMaxRoot(4, g, isMax);
                //isMax = !isMax;
                g.Move(bestMove.From, bestMove.To);
                string name = Util.GetPieceName(bestMove.From).ToString();
                string fileTo = Util.ShortToFile(Util.GetXForPiece(bestMove.To)).ToString();
                string rankTo = (Util.GetYForPiece(bestMove.To) + 1).ToString();
                sb.Append(name + fileTo + rankTo + " : " + g.BoardValue() + "\r\n");

                s = g.ToString();
                sb.Append(s);
                sb.Append("\r\n\r\n");

            }
            w.Stop();
            textBox3.Text += w.ElapsedMilliseconds + "ms for " + count + " options\r\n\r\n";
            textBox3.Text += sb.ToString();
        }

        public Move MinMaxRoot(int depth, Game g, bool IsMaximizingPlayer)
        {
            List<Move> moves = g.GetMoves();
            moves.Shuffle();
            int bestMove = -9999;
            Move bestMoveFound = moves.FirstOrDefault();

            string FENFEN = g.ToFENString();
            //foreach (Move move in moves)
            //{
            List<Tuple<Move, int>> returnVal = (moves.AsParallel().Select((move) =>
            {
                Game g2 = new Game(FENFEN);
                g2.Move(move.From, move.To);

                int value = MiniMax(depth - 1, g2, -10000, 10000, !IsMaximizingPlayer);

                g2.Undo();


                return new Tuple<Move, int>(bestMoveFound, value);
            })).ToList();

            foreach(var moveTuple in returnVal)
            {
                if (moveTuple.Item2 >= bestMove)
                {
                    bestMove = moveTuple.Item2;
                    bestMoveFound = moveTuple.Item1;
                }
            }
            //}

            return bestMoveFound;
        }
        public int MiniMax(int depth, Game g, int alpha, int beta, bool IsMaximizingPlayer)
        {
            if (depth == 0)
            {
                return -1*(g.BoardValue());
            }

            List<Move> moves = g.GetMoves();

            if (IsMaximizingPlayer)
            {
                int bestMove = -9999;
                foreach (Move move in moves)
                {

                    g.Move(move.From, move.To);
                    bestMove = Math.Max(bestMove, MiniMax(depth - 1, g, alpha, beta, !IsMaximizingPlayer));
                    g.Undo();
                    alpha = Math.Max(alpha, bestMove);
                    if (beta <= alpha)
                    {
                        return bestMove;
                    }
                }
                return bestMove;
            }
            else
            {
                int bestMove = 9999;
                foreach (Move move in moves)
                {
                    g.Move(move.From, move.To);
                    bestMove = Math.Min(bestMove, MiniMax(depth - 1, g, alpha, beta, !IsMaximizingPlayer));
                    g.Undo();
                    beta = Math.Min(beta, bestMove);
                    if (beta <= alpha)
                    {
                        return bestMove;
                    }
                }
                return bestMove;
            }
        }


        public short GetBestMove(int depth, Game g)
        {
            if (depth == 0)
            {
                return (short)g.BoardValue();
            }
            List<string> result = new List<string>();
            List<Move> mov = g.GetMoves();
            short bestValue = g.WhiteToMove ? (short)-9999 : (short)9999;

            foreach (Move move in mov)
            {
                count++;
                g.Move(move.From, move.To);
                short newValue = GetBestMove(depth - 1, g);
                g.Undo();
                if ((g.WhiteToMove && newValue > bestValue) || (!g.WhiteToMove && newValue < bestValue))
                {
                    bestValue = newValue;
                }
            }
            return bestValue;
        }
    }
}
