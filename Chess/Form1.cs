using System;
using System.Windows.Forms;
using System.Diagnostics;
using Chess.Models;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Chess
{
    public partial class Form1 : Form
    {
        int count = 0;
        StringBuilder sb = new StringBuilder();
        Game mainGame;
        Stopwatch w = new Stopwatch();
        public Form1()
        {
            InitializeComponent();

        }
        public void Undo()
        {
            mainGame.Undo();
            sb.Append(mainGame.ToString());
            sb.Append("\r\n\r\n");
            sb.Append(mainGame.ToFENString());
            sb.Append("\r\n\r\n");
            textBox3.AppendText(sb.ToString());
            sb.Clear();
        }
        public void MakeMove()
        {
            double initialValue = mainGame.BoardValue();
            count = 0;
            w.Start();
            try
            {
                Move bestMove = MinMaxRoot(mainGame.WhiteToMove?(int)numericUpDownWhite.Value: (int)numericUpDownBlack.Value, mainGame, true);

               //Move bestMove = newRoot(3, mainGame);

                bestMove = mainGame.Move(bestMove);
                string name = Util.GetPieceName(bestMove.From).ToString();
                string fileTo = Util.IntToFile(Util.GetXForPiece(bestMove.To)).ToString();
                string rankTo = (Util.GetYForPiece(bestMove.To) + 1).ToString();
                string move = string.Format("{3} From:{0}, To:{1}({4},{5}), Capturing:{2}, Value:{6}\r\n", bestMove.From, bestMove.To, bestMove.Captured, name, fileTo, rankTo, mainGame.BoardValue());
                sb.Append(move);

                sb.Append(mainGame.ToString());
                sb.Append("\r\n\r\n");
                sb.Append(mainGame.ToFENString());
                sb.Append("\r\n\r\n");
                w.Stop();
                sb.Append(w.ElapsedMilliseconds + "ms for " + count + " options. " + (initialValue - mainGame.BoardValue()) + " change in value.\r\n\r\n");
                sb.Append(count / (w.ElapsedMilliseconds + 1) + " moves per ms.\r\n\r\n");
                w.Reset();
                textBox3.AppendText(sb.ToString());
                sb.Clear();
            }
            catch (StalemateException ex)
            {
                textBox3.AppendText("\r\n\r\nSTALEMATE!");
                throw ex;
            }
            catch (CheckmateException ex)
            {
                textBox3.AppendText("\r\n\r\nCHECKMATE!");
                throw ex;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            string value = textBox2.Text;
            Game g;
            g = new Game(value);
            sb.Append(g.ToString());
            sb.Append("\r\n\r\n");
            mainGame = g;
            textBox3.AppendText(sb.ToString());
        }

        public Move newRoot(int depth, Game g)
        {
            MoveGenerationResult result = g.GetMoves();
            Random r = new Random();

            count += result.Moves.Count;
            if (result.Endgame == EndgameType.Checkmate)
            {
                throw new CheckmateException();
            }
            else if (result.Endgame == EndgameType.Stalemate)
            {
                throw new StalemateException();
            }
            Move bestMoveFound = result.Moves[r.Next(result.Moves.Count - 1)];

            double bestMove = g.WhiteToMove ? -999999 : 999999;

            string FENFEN = g.ToFENString();

            var exceptions = new ConcurrentQueue<Exception>();
            ConcurrentBag<Move> resultVal = new ConcurrentBag<Move>();
            Parallel.ForEach(result.Moves, (move) =>
            {
                Game g2 = new Game(FENFEN);
                g2.Move(move);
                try
                {
                    move.MaterialScore = NewMiniMax(depth - 1, g2, move).MaterialScore;
                    resultVal.Add(move);
                }
                catch (Exception w)
                {
                    exceptions.Enqueue(w);
                }
                g2.Undo();
            });

            if (exceptions.Count > 0 && resultVal.Count == 0)
            {
                Exception ex;
                exceptions.TryDequeue(out ex);
                throw ex;
            }

            foreach (Move moveTuple in resultVal)
            {
                if ((g.WhiteToMove && (moveTuple.MaterialScore >= bestMove)) || (!g.WhiteToMove && (moveTuple.MaterialScore <= bestMove)))
                {
                    bestMove = moveTuple.MaterialScore;
                    bestMoveFound = moveTuple;
                }
            }

            return bestMoveFound;
        }
        public Move NewMiniMax(int depth, Game g, Move thisMove)
        {
            MoveGenerationResult result = g.GetMoves();
            if (depth == 0)
            {
                if (result.Moves.Count == 0)
                {
                    thisMove.MaterialScore = g.WhiteToMove ? -1000000 : 1000000;
                    return thisMove;
                }
                thisMove.MaterialScore = result.Moves.Count * thisMove.MaterialScore;
                return thisMove;
            }
            count += result.Moves.Count;
            Move theMove = result.Moves[0];

            double material = g.BoardValue();

            if (g.WhiteToMove)
            {
                double bestMove = -99999;
                foreach (Move move in result.Moves)
                {
                    g.Move(move);
                    var nextMove = NewMiniMax(depth - 1, g, move);
                    if (nextMove.MaterialScore >= bestMove)
                    {
                        bestMove = move.MaterialScore;
                        theMove = move;
                    }else if (nextMove.MaterialScore < bestMove)
                    {
                        g.Undo();
                        break;
                    }
                    g.Undo();
                }
                return theMove;
            }
            else
            {

                double bestMove = 99999;

                foreach (Move move in result.Moves)
                {
                    g.Move(move);
                    var nextMove = NewMiniMax(depth - 1, g, move);
                    if (nextMove.MaterialScore <= bestMove)
                    {
                        bestMove = move.MaterialScore;
                        theMove = move;
                    }
                    else if (nextMove.MaterialScore > bestMove)
                    {
                        g.Undo();
                        break;
                    }
                    g.Undo();

                }

                return theMove;
            }
        }
        public Move MinMaxRoot(int depth, Game g, bool IsMaximizingPlayer)
        {

            MoveGenerationResult result = g.GetMoves();

            count += result.Moves.Count;
            if (result.Endgame == EndgameType.Checkmate)
            {
                throw new CheckmateException();
            }
            else if (result.Endgame == EndgameType.Stalemate)
            {
                throw new StalemateException();
            }

            double bestMove = -999999;
            Random r = new Random();
            Move bestMoveFound = result.Moves[r.Next(result.Moves.Count - 1)];

            string FENFEN = g.ToFENString();

            var exceptions = new ConcurrentQueue<Exception>();
            ConcurrentBag<Tuple<Move, double>> resultVal = new ConcurrentBag<Tuple<Models.Move, double>>();
            Parallel.ForEach(result.Moves, (move) =>
            {
                Game g2 = new Game(FENFEN);
                g2.Move(move);
                try
                {
                    double value = MiniMax(depth - 1, g2, -1000000, 1000000, !IsMaximizingPlayer, move.To);
                    resultVal.Add(new Tuple<Move, double>(move, value));
                }
                catch (Exception w)
                {
                    exceptions.Enqueue(w);
                }
                g2.Undo();
            });

            if (exceptions.Count > 0 && resultVal.Count == 0)
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

            return bestMoveFound;
        }
        public double MiniMax(int depth, Game g, double alpha, double beta, bool IsMaximizingPlayer, int thisMove)
        {
            if (depth == 0)
            {
                return g.Material() ;
            }


            MoveGenerationResult result = g.GetMoves();
            count += result.Moves.Count;

            if (IsMaximizingPlayer)
            {
                int multi = g.WhiteToMove ? 1 : -1;
                double bestMove = -999999;

                foreach (Move move in result.Moves)
                {

                        g.Move(move);
                        bestMove = Math.Max(bestMove, MiniMax(depth - 1, g, alpha, beta, !IsMaximizingPlayer, move.To));
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
                int multi = g.WhiteToMove ? 1 : -1;
                double bestMove = 999999;
                foreach (Move move in result.Moves)
                {

                        g.Move(move);
                        bestMove = Math.Min(bestMove, MiniMax(depth - 1, g, alpha, beta, !IsMaximizingPlayer, move.To));
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


        private void button1_Click(object sender, EventArgs e)
        {
            //mainGame.Undo();
            //MoveGenerationResult result = mainGame.GetMoves();
            //StringBuilder sb = new StringBuilder();
            //foreach(Move m in result.Moves)
            //{
            //    string name = Util.GetPieceName(m.From).ToString();
            //    string fileTo = Util.IntToFile(Util.GetXForPiece(m.To)).ToString();
            //    string rankTo = (Util.GetYForPiece(m.To) + 1).ToString();
            //    string move = string.Format("{2} From:{0}, To:{1}({3},{4})\r\n", m.From, m.To, name, fileTo, rankTo);
            //    sb.Append(move);
            //}

            //textBox3.Text += sb.ToString();
            PrintPGN();
        }
        public void PrintPGN()
        {
            sb.Clear();
            int turn = 0;
            int num = 1;
            foreach (Move m in mainGame.history.Reverse())
            {
                string name = Util.GetPieceProperName(m.From).ToString();
                string fileTo = Util.IntToFile(Util.GetXForPiece(m.To)).ToString();
                string fileFrom = Util.IntToFile(Util.GetXForPiece(m.From)).ToString();
                string rankTo = (Util.GetYForPiece(m.To) + 1).ToString();
                string rankFrom = (Util.GetYForPiece(m.From) + 1).ToString();
                string cap = m.Captured.HasValue ? "x" : "";
                name += fileFrom + rankFrom;
                string numStr = turn % 2 == 0 ? "\r\n" + num.ToString() + ". " : " ";
                string move = string.Format("{0}{1}{2}{3}{4} ", numStr, name, cap, fileTo, rankTo);
                if (turn % 2 == 0)
                {
                    num++;
                }
                sb.Append(move);

                turn++;
            }
            textBox3.AppendText(sb.ToString());
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                MakeMove();
            }
            catch (StalemateException ex)
            {
            }
            catch (CheckmateException ex)
            {
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int m = 200;
            for (int i = 0; i < m; i++)
            {
                try
                {
                    MakeMove();
                }
                catch (StalemateException ex)
                {
                    i = m;
                }
                catch (CheckmateException ex)
                {
                    i = m;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Undo();
        }
    }
}
