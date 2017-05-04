using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public class Game
    {
        private List<short> _positions;
        private bool _whiteOOCastle = true;
        private bool _whiteOOOCastle = true;
        private bool _blackOOCastle = true;
        private bool _blackOOOCastle = true;
        private bool _whiteToMove = true;
        private short? enPassantSquare;
        private Stack<Tuple<short, short>> history;
        public List<short> Positions
        {
            get { return _positions; }
        }
        public bool WhiteOOCastle
        {
            get
            {
                return _whiteOOCastle;
            }
        }
        public bool WhiteOOOCastle
        {
            get
            {
                return _whiteOOOCastle;
            }
        }
        public bool BlackOOCastle
        {
            get
            {
                return _blackOOCastle;
            }
        }
        public bool BlackOOOCastle
        {
            get
            {
                return _blackOOOCastle;
            }
        }
        public bool WhiteToMove
        {
            get
            {
                return _whiteToMove;
            }
        }

        public Game()
        {
            history = new Stack<Tuple<short, short>>();
            _positions = new short[32]{
                704,577,642,771,836,645,582,711,    //white royal
                520,521,522,523,524,525,526,527,    //white pawns
                48,49,50,51,52,53,54,55,            //black pawns
                248,121,186,315,380,189,126,255     //black royal
            }.ToList();
        }
        public Game(string fen)
        {
            history = new Stack<Tuple<short, short>>();
            //split the sections of the FEN string with spaces
            string[] fenParts = fen.Split(' ');

            //first part is for piece positions
            if (fenParts.Length > 0)
            {
                short[] positions = new short[64];
                int index = 0;
                //rows are delimited by /
                //Row order reversed because the convention I am using is bottom-left=0,0, but FEN starts with rank 8 for some stupid reason
                string[] positionRows = fenParts[0].Split('/').Reverse().ToArray();
                for (int y = 0; y < 8; y++)
                {
                    string row = positionRows[y];
                    //and we have to reverse the row itself too.
                    char[] charArray = row.ToCharArray();
                    Array.Reverse(charArray);
                    row = new string(charArray);

                    for (int x = 0; x < row.Length; x++)
                    {
                        //if we see a number, that means n spots are empty in this row, skip over them
                        if (Char.IsNumber(row[x]))
                        {
                            //-'0' char trick to offset ascii number
                            index += row[x] - '0';
                        }
                        else
                        {
                            positions[index] = (short)(PieceTypeFENMap.PieceValue(row[x]) + index);
                            index++;
                        }
                    }
                }
                _positions = positions.Where(e => e != 0).ToList();
            }
            if (fenParts.Length > 1)
            {
                if (fenParts[1] == "b")
                {
                    _whiteToMove = false;
                }
            }
            if (fenParts.Length > 2)
            {
                if (!fenParts[2].Contains("K"))
                {
                    _whiteOOCastle = false;
                }
                if (!fenParts[2].Contains("Q"))
                {
                    _whiteOOOCastle = false;
                }
                if (!fenParts[2].Contains("k"))
                {
                    _blackOOCastle = false;
                }
                if (!fenParts[2].Contains("q"))
                {
                    _blackOOOCastle = false;
                }
            }
            if (fenParts.Length > 3 && fenParts[3][0] !='-')
            {
                //file + (8 * rank)
                //ascii offset -'1' instead of -'0' because FEN is 1 indexed instead of 0 indexed
                enPassantSquare = (short)(Util.FileToShort(fenParts[3][0]) + (8 * (fenParts[3][1]-'1')));
            }
        }
        public Dictionary<short, List<short>> GetMoves()
        {
            List<short> moves = new List<short>();
            Dictionary<short, List<short>> pieceMoveMap = new Dictionary<short, List<short>>();
            MoveGenerator mg = new MoveGenerator();
            foreach(short piece in _positions)
            {
                pieceMoveMap.Add(piece, mg.GenerateMovesForPiece(piece));

            }
            return pieceMoveMap;
        }


        public void Move(short currentPiece, short newPiece)
        {
            if (_positions.Contains(currentPiece))
            {
                _positions.Remove(currentPiece);
                _positions.Add(newPiece);
                history.Push(new Tuple<short, short>(currentPiece, newPiece));
            }
        }
        public void Undo()
        {
            Tuple<short,short> move=history.Pop();
            if (_positions.Contains(move.Item2))
            {
                _positions.Remove(move.Item2);
                _positions.Add(move.Item1);                
            }
        }
        public string ToFENString()
        {
            string result = "";

            string[] resultArr = new string[8 * 8];
            foreach (short piece in _positions)
            {
                resultArr[Util.GetPieceOffset(piece)] = PieceTypeFENMap.PieceName(piece).ToString();
            }
            int count = 0;
            StringBuilder sb = new StringBuilder();
            //we have to re-reverse the result for text-box
            int skipGrabber = 0;
            foreach (string p in resultArr.Reverse())
            {

                if (p != null)
                {
                    if (skipGrabber != 0)
                    {
                        sb.Append(skipGrabber);
                        skipGrabber = 0;
                    }
                    sb.Append(p);
                }
                else
                {
                    skipGrabber++;
                }
                
                count++;
                if (count % 8 == 0)
                {
                    if (skipGrabber != 0)
                    {
                        sb.Append(skipGrabber);
                        skipGrabber = 0;
                    }
                    sb.Append("/");
                }
            }

            //which player has next move
            sb.Append(" ");
            if (_whiteToMove)
            {
                sb.Append("w");
            }
            else
            {
                sb.Append("b");
            }

            //castling
            sb.Append(" ");
            if(_whiteOOCastle|| _whiteOOOCastle || _blackOOCastle || _blackOOOCastle)
            {
                if (_whiteOOCastle)
                {
                    sb.Append("K");
                }
                if (_whiteOOOCastle)
                {
                    sb.Append("Q");
                }
                if (_blackOOCastle)
                {
                    sb.Append("k");
                }
                if (_blackOOOCastle)
                {
                    sb.Append("q");
                }
            }else
            {
                sb.Append("-");
            }

            sb.Append(" ");
            if (enPassantSquare.HasValue)
            {
                sb.Append(Util.ShortToFile(Util.GetXForPosition((byte)enPassantSquare.Value)));
                sb.Append(Util.GetYForPosition((byte)enPassantSquare.Value)+1);
            }
            return sb.ToString();
        }
        public override string ToString()
        {
            string[] resultArr = new string[8 * 8];
            foreach (short piece in _positions)
            {
                resultArr[Util.GetPieceOffset(piece)] = PieceTypeFENMap.PieceName(piece).ToString();
            }
            int count = 0;
            StringBuilder sb = new StringBuilder();
            //we have to re-reverse the result for text-box
            foreach (string p in resultArr.Reverse())
            {
                sb.Append(p ?? "-");
                count++;
                if (count % 8 == 0)
                {
                    sb.Append("\r\n");
                }
            }
            return sb.ToString();
        }

    }
}
