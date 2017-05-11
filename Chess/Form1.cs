using System;
using System.Windows.Forms;
using System.Diagnostics;
using Chess.Models;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;

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
        Game mainGame;

        private void button3_Click(object sender, EventArgs e)
        {
            string value = textBox2.Text;
            Game g;
            int howmany = 100000;
            StringBuilder sb = new StringBuilder();
            Stopwatch w = new Stopwatch();
            w.Start();
            g = new Game(value);
            mainGame = g;
            string s = "";
            count = 0;
            textBox3.Text = "";

            bool isMax = true;
            string initialValue = g.BoardValue().ToString();
            bool isSucky = true;
            int maxMoves = 150;
            for (int jj = 0; jj < maxMoves; jj++)
            {
                try
                {
                    Move bestMove = MinMaxRoot(isSucky?2:3, g, isMax);
                    if (bestMove==null)
                    {

                        throw new Exception();

                    }
                    bestMove=g.Move(bestMove);
                    string name = Util.GetPieceName(bestMove.From).ToString();
                    string fileTo = Util.ShortToFile(Util.GetXForPiece(bestMove.To)).ToString();
                    string rankTo = (Util.GetYForPiece(bestMove.To) + 1).ToString();
                    string move = string.Format("{3} From:{0}, To:{1}({4},{5}), Capturing:{2}, Value:{6}\r\n", bestMove.From, bestMove.To, bestMove.Captured, name, fileTo, rankTo, g.BoardValue());
                    sb.Append(move);

                    s = g.ToString();
                    sb.Append(g.ToString());
                    sb.Append("\r\n\r\n");
                    sb.Append(g.ToFENString());
                    sb.Append("\r\n\r\n");
                }
                catch(StalemateException ex)
                {
                    sb.Append("\r\n\r\nSTALEMATE!");
                    jj = maxMoves;
                }
                catch (CheckmateException ex)
                {
                    sb.Append("\r\n\r\nCHECKMATE!");
                    jj = maxMoves;
                }
                //isMax = !isMax;

                isSucky = !isSucky;

            }
            w.Stop();
            textBox3.Text += w.ElapsedMilliseconds + "ms for " + count + " options. "+ initialValue+"\r\n\r\n";
            textBox3.Text += sb.ToString();
            int turn = 0;
            int num = 1;
            foreach(Move m in g.history.Reverse())
            {
                string name = Util.GetPieceProperName(m.From).ToString();
                string fileTo = Util.ShortToFile(Util.GetXForPiece(m.To)).ToString();
                string fileFrom = Util.ShortToFile(Util.GetXForPiece(m.From)).ToString();
                string rankTo = (Util.GetYForPiece(m.To) + 1).ToString();
                string rankFrom = (Util.GetYForPiece(m.From) + 1).ToString();
                string cap = m.Captured.HasValue ? "x" : "";
                name += fileFrom + rankFrom; 
                string numStr = turn % 2 == 0 ? "\r\n" +num.ToString() + ". " : " ";
                string move = string.Format("{0}{1}{2}{3}{4} ", numStr, name,cap, fileTo, rankTo);
                if (turn % 2 == 0)
                {
                    num++;
                }
                textBox3.Text += move;

                turn++;
            }
            textBox3.Text += "\r\n" + g.ToFENString() ;
        }

        public Move MinMaxRoot(int depth, Game g, bool IsMaximizingPlayer)
        {

            List<Move> moves = g.GetMoves();

            moves.Shuffle();
            double bestMove = -9999;
            Random r = new Random();
            Move bestMoveFound = moves[r.Next(moves.Count-1)];

            string FENFEN = g.ToFENString();
            //foreach (Move move in moves)
            //{
            var exceptions = new ConcurrentQueue<Exception>();
            ConcurrentBag<Tuple<Move, double>> resultVal = new ConcurrentBag<Tuple<Models.Move, double>>();
            Parallel.ForEach(moves, (move) =>
            {
                Game g2 = new Game(FENFEN);
                g2.Move(move);
                try
                {
                    double value = MiniMax(depth - 1, g2, -10000, 10000, !IsMaximizingPlayer);
                    resultVal.Add(new Tuple<Move, double>(move, value));
                }
                catch (Exception w)
                {
                    exceptions.Enqueue(w);
                }
                g2.Undo();


            });
            if (exceptions.Count > 0 && resultVal.Count==0)
            {
                Exception ex;
                exceptions.TryDequeue(out ex);
                throw ex;
            }

            foreach (Tuple<Move, double> moveTuple in resultVal)
            {
                if (moveTuple.Item2 >= bestMove)
                {
                    bestMove = moveTuple.Item2;
                    bestMoveFound = moveTuple.Item1;
                }
            }
           // }

            return bestMoveFound;
        }
        public double MiniMax(int depth, Game g, double alpha, double beta, bool IsMaximizingPlayer)
        {
            if (depth == 0)
            {
                return -g.BoardValue();
            }

            List<Move> moves = g.GetMoves();
            //if (depth == 1)
            //{
            //    moves.Shuffle();
            //}
            if (IsMaximizingPlayer)
            {
                double bestMove = -9999;
                foreach (Move move in moves)
                {

                    g.Move(move);
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
                double bestMove = 9999;
                foreach (Move move in moves)
                {
                    g.Move(move);
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
                g.Move(move);
                short newValue = GetBestMove(depth - 1, g);
                g.Undo();
                if ((g.WhiteToMove && newValue > bestValue) || (!g.WhiteToMove && newValue < bestValue))
                {
                    bestValue = newValue;
                }
            }
            return bestValue;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainGame.Undo();
            List<Move> moves = mainGame.GetMoves();
            StringBuilder sb = new StringBuilder();
            foreach(Move m in moves)
            {
                string name = Util.GetPieceName(m.From).ToString();
                string fileTo = Util.ShortToFile(Util.GetXForPiece(m.To)).ToString();
                string rankTo = (Util.GetYForPiece(m.To) + 1).ToString();
                string move = string.Format("{2} From:{0}, To:{1}({3},{4})\r\n", m.From, m.To, name, fileTo, rankTo);
                sb.Append(move);
            }
       
            textBox3.Text += sb.ToString();
        }
    }
}
