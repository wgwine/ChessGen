using System;
using System.Windows.Forms;
using System.Diagnostics;
using Chess.Models;
using System.Collections.Generic;
using System.Text;

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

        private void button1_Click(object sender, EventArgs e)
        {
            Stopwatch w = new Stopwatch();
            short value = (short)numericUpDown2.Value;
            textBox1.Text = gs.GetPieceStringFromShort(value);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Stopwatch w = new Stopwatch();
            string[] value = textBox1.Text.Split('-');

            numericUpDown2.Value = gs.GetShortFromPieceString(textBox1.Text);
        }
        public PieceType GetPieceType(short piece)
        {
            return (PieceType)ByteFromBoolArray(new bool[]{ CheckPosition(piece, 8), CheckPosition(piece, 7), CheckPosition(piece, 6) });
        }
        public bool CheckPosition(short value,byte index)
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
            string s="";
            for (int i = 0; i < howmany; i++) {

                Dictionary<short, List<short>> mov =g.GetMoves();
                //foreach(KeyValuePair<short, List<short>> move in mov)
                //{
                //    foreach(short newMove in move.Value)
                //    {
                //        g.Move(move.Key, newMove);
                //        s = g.ToString();
                //        sb.Append(s);
                //        sb.Append("\r\n\r\n");

                //        g.Undo();
                //    }
                //}                
            }
            w.Stop();

            textBox3.Text = w.ElapsedMilliseconds+"ms for "+ howmany+" parses";
            textBox3.Text += sb.ToString();
        }

    }
}
