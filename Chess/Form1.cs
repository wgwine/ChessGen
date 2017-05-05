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
        public Form1()
        {
            InitializeComponent();
            gs = new GameService();
        }

        public PieceType GetPieceType(short piece)
        {
            return (PieceType)ByteFromBoolArray(new bool[] { CheckPosition(piece, 8), CheckPosition(piece, 7), CheckPosition(piece, 6) });
        }
        public bool CheckPosition(short value, byte index)
        {
            return (value & (1 << index)) != 0;
        }
        byte ByteFromBoolArray(bool[] arr)
        {
            byte result = 0;
            foreach (bool b in arr)
            {
                result <<= 1;
                if (b) result |= 1;
            }
            return result;
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
            int count = 0;
            textBox3.Text = "";
            short defaultValue = 9999;
            short bestValue = -9999;
            Tuple<short, short> bestMove = null;
            for (int jj = 0; jj < 500; jj++)
            {
                IEnumerable<Tuple<short, List<short>>> mov = g.GetMoves();
                defaultValue = (short)(-1 * defaultValue);
                bestValue = defaultValue;


                string FENFEN = g.ToFENString();
                List<Tuple<short, short, short>> returnVal = (mov.Where(m => m.Item2.Count > 0).AsParallel().Select((move) =>
                {
                    Game g2 = new Game(FENFEN);
                    short bestV = g2.WhiteToMove ? (short)-9999 : (short)9999;
                    Tuple<short, short, short> bestM = new Tuple<short, short, short>(0, 0, 0);
                    foreach (var newMove in move.Item2)
                    {
                        count++;
                        g2.Move(move.Item1, newMove);
                        short returnValue = GetBestMove(2, g2);
                        g2.Undo();
                        if ((g2.WhiteToMove && returnValue < bestV) || (!g2.WhiteToMove && returnValue > bestV))
                        {
                            bestM = new Tuple<short, short, short>(move.Item1, newMove, returnValue);
                            bestV = returnValue;
                        }
                    }
                    return bestM;
                })).ToList();



                foreach (Tuple<short, short, short> newMove in returnVal)
                {
                    count++;

                    if ((g.WhiteToMove && newMove.Item3 > bestValue) || (!g.WhiteToMove && newMove.Item3 < bestValue))
                    {
                        bestMove = new Tuple<short, short>(newMove.Item1, newMove.Item2);
                        bestValue = newMove.Item3;
                    }

                }
                string name = Util.GetPieceName(bestMove.Item1).ToString();
                string fileTo = Util.ShortToFile(Util.GetXForPiece(bestMove.Item2)).ToString();
                string rankTo = (Util.GetYForPiece(bestMove.Item2) + 1).ToString();
                sb.Append(name + fileTo + rankTo + " : " + g.BoardValue() + "\r\n");
                g.Move(bestMove.Item1, bestMove.Item2);
                s = g.ToString();
                sb.Append(s);
                sb.Append("\r\n\r\n");
            }



            //for (int i = 0; i < howmany; i++) {
            //    IEnumerable<KeyValuePair<short, List<short>>> mov = g.GetMoves();

            //foreach (KeyValuePair<short, List<short>> move in mov)
            //{
            //    foreach (short newMove in move.Value)
            //    {
            //        count++;
            //        g.Move(move.Key, newMove);
            //        s = g.ToString();
            //        sb.Append(s);
            //        sb.Append("\r\n\r\n");

            //        g.Undo();
            //    }
            //}
            //}
            w.Stop();

            textBox3.Text += w.ElapsedMilliseconds + "ms for " + count + " options\r\n\r\n";

            textBox3.Text += sb.ToString();
        }
        public List<string> RecurseMoves(int depth, Game g)
        {
            if (depth == 0)
            {
                return new List<string>() { g.ToFENString() };
            }
            List<string> result = new List<string>();
            IEnumerable<Tuple<short, List<short>>> mov = g.GetMoves();
            foreach (Tuple<short, List<short>> move in mov)
            {
                foreach (short newMove in move.Item2)
                {
                    g.Move(move.Item1, newMove);
                    result.AddRange(RecurseMoves(depth - 1, g));
                    g.Undo();
                }
            }
            return result;
        }
        public short GetBestMove(int depth, Game g)
        {
            if (depth == 0)
            {
                return (short)g.BoardValue();
            }
            List<string> result = new List<string>();
            IEnumerable<Tuple<short, List<short>>> mov = g.GetMoves();
            short bestValue = g.WhiteToMove ? (short)-9999 : (short)9999;
            Tuple<short, short, short> bestReturn = new Tuple<short, short, short>(0, 0, 0);
            foreach (Tuple<short, List<short>> move in mov)
            {
                foreach (short newMove in move.Item2)
                {
                    g.Move(move.Item1, newMove);
                    short newValue = GetBestMove(depth - 1, g);
                    g.Undo();

                    if ((g.WhiteToMove && newValue > bestValue) || (!g.WhiteToMove && newValue < bestValue))
                    {
                        bestValue = newValue;
                        bestReturn = new Tuple<short, short, short>(move.Item1, newMove, newValue);
                    }

                }
            }
            return bestValue;
        }
    }
}
