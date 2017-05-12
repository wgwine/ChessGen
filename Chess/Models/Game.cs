using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public class Game
    {
        double[][] knightEval = new double[8][]{
            new double[]{-5.0, -4.0, -3.0, -3.0, -3.0, -3.0, -4.0, -5.0},
            new double[]{-4.0, -2.0, 0.0, 0.0, 0.0, 0.0, -2.0, -4.0},
            new double[]{-3.0, 0.0, 1.0, 1.5, 1.5, 1.0, 0.0, -3.0},
            new double[]{-3.0, 0.5, 1.5, 2.0, 2.0, 1.5, 0.5, -3.0},
            new double[]{-3.0, 0.0, 1.5, 2.0, 2.0, 1.5, 0.0, -3.0},
            new double[]{-3.0, 0.5, 1.0, 1.5, 1.5, 1.0, 0.5, -3.0},
            new double[]{-4.0, -2.0, 0.0, 0.5, 0.5, 0.0, -2.0, -4.0},
            new double[]{-5.0, -4.0, -3.0, -3.0, -3.0, -3.0, -4.0, -5.0}
        };
        double[][] bishopEvalBlack = new double[8][]{
            new double[]{-2.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -2.0},
            new double[]{ -1.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -1.0},
            new double[]{-1.0,  0.0,  0.5,  1.0,  1.0,  0.5,  0.0, -1.0},
            new double[]{-1.0,  0.5,  0.5,  1.0,  1.0,  0.5,  0.5, -1.0},
            new double[]{ -1.0,  0.0,  1.0,  1.0,  1.0,  1.0,  0.0, -1.0},
            new double[]{-1.0,  1.0,  1.0,  1.0,  1.0,  1.0,  1.0, -1.0},
            new double[]{ -1.0,  0.5,  0.0,  0.0,  0.0,  0.0,  0.5, -1.0},
            new double[]{ -2.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -2.0}
        };
        double[][] pawnEvalBlack = new double[8][]{
            new double[]{0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0},
            new double[]{5.0,  5.0,  5.0,  5.0,  5.0,  5.0,  5.0,  5.0},
            new double[]{1.0,  1.0,  2.0,  3.0,  3.0,  2.0,  1.0,  1.0},
            new double[]{0.5,  0.5,  1.0,  2.5,  2.5,  1.0,  0.5,  0.5},
            new double[]{0.0,  0.0,  0.0,  2.0,  2.0,  0.0,  0.0,  0.0},
            new double[]{0.5, -0.5, -1.0,  0.0,  0.0, -1.0, -0.5,  0.5},
            new double[]{0.5,  1.0, 1.0,  -2.0, -2.0,  1.0,  1.0,  0.5},
            new double[]{ 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 }
        };
        double[][] rookEvalBlack = new double[8][]{
            new double[]{0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0},
            new double[]{ 0.5,  1.0,  1.0,  1.0,  1.0,  1.0,  1.0,  0.5},
            new double[]{ -0.5,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -0.5},
            new double[]{ -0.5,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -0.5},
            new double[]{-0.5,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -0.5},
            new double[]{ -0.5,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -0.5},
            new double[]{-0.5,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -0.5},
            new double[]{  0.0,   0.0, 0.0,  0.5,  0.5,  0.0,  0.0,  0.0}
        };
        double[][] queenEval = new double[8][]{
            new double[]{ -2.0, -1.0, -1.0, -0.5, -0.5, -1.0, -1.0, -2.0},
            new double[]{ -1.0,  0.0,  0.0,  0.0,  0.0,  0.0,  0.0, -1.0},
            new double[]{ -1.0,  0.0,  0.5,  0.5,  0.5,  0.5,  0.0, -1.0},
            new double[]{ -0.5,  0.0,  0.5,  0.5,  0.5,  0.5,  0.0, -0.5},
            new double[]{  0.0,  0.0,  0.5,  0.5,  0.5,  0.5,  0.0, -0.5},
            new double[]{ -1.0,  0.5,  0.5,  0.5,  0.5,  0.5,  0.0, -1.0},
            new double[]{ -1.0,  0.0,  0.5,  0.0,  0.0,  0.0,  0.0, -1.0},
            new double[]{ -2.0, -1.0, -1.0, -0.5, -0.5, -1.0, -1.0, -2.0 }
        };
        double[][] kingEvalBlack = new double[8][]{
            new double[]{  -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0},
            new double[]{ -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0},
            new double[]{-3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0},
            new double[]{  -3.0, -4.0, -4.0, -5.0, -5.0, -4.0, -4.0, -3.0},
            new double[]{   -2.0, -3.0, -3.0, -4.0, -4.0, -3.0, -3.0, -2.0},
            new double[]{  -1.0, -2.0, -2.0, -2.0, -2.0, -2.0, -2.0, -1.0},
            new double[]{  2.0,  2.0,  0.0,  0.0,  0.0,  0.0,  2.0,  2.0},
            new double[]{  2.0,  3.0,  1.0,  0.0,  0.0,  1.0,  3.0,  2.0  }
        };
        double[][] pawnEvalWhite;
        double[][] bishopEvalWhite;
        double[][] rookEvalWhite;
        double[][] kingEvalWhite;
        private List<int> _positions;
        private int[] _theBoard;
        private bool _whiteOOCastle = true;
        private bool _whiteOOOCastle = true;
        private bool _blackOOCastle = true;
        private bool _blackOOOCastle = true;
        private bool _whiteToMove = true;
        private int? enPassantSquare; int whiteKing = 0, blackKing = 0, currentKingPiece;
        public Stack<Move> history;
        public List<int> Positions
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
            history = new Stack<Move>();
            _positions = new int[32]{
                704,577,642,771,836,645,582,711,    //white royal
                520,521,522,523,524,525,526,527,    //white pawns
                48,49,50,51,52,53,54,55,            //black pawns
                248,121,186,315,380,189,126,255     //black royal
            }.ToList();
            bishopEvalBlack = bishopEvalWhite.Reverse().ToArray();
        }
        public Game(string fen)
        {
            _theBoard = new int[64];
            history = new Stack<Move>();
            bishopEvalWhite = bishopEvalBlack.Reverse().ToArray();
            pawnEvalWhite = pawnEvalBlack.Reverse().ToArray();
            rookEvalWhite = rookEvalBlack.Reverse().ToArray();
            kingEvalWhite = kingEvalBlack.Reverse().ToArray();
            //split the sections of the FEN string with spaces
            string[] fenParts = fen.Trim().Split(' ');

            //first part is for piece positions
            if (fenParts.Length > 0)
            {
                int[] positions = new int[64];
                int index = 0;
                //rows are delimited by /
                //Row order reversed because the convention I am using is bottom-left=0,0, but FEN starts with rank 8 for some stupid reason
                string[] positionRows = fenParts[0].Trim().Split('/').Reverse().ToArray();
                for (int y = 0; y < 8; y++)
                {
                    string row = positionRows[y];
                    //and we have to reverse the row itself too.
                    char[] charArray = row.ToCharArray();

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
                            positions[index] = (PieceTypeFENMap.PieceValue(row[x]) + index);
                            index++;
                        }
                    }
                }
                _theBoard = positions;
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
            if (fenParts.Length > 3 && fenParts[3][0] != '-')
            {
                //file + (8 * rank)
                //ascii offset -'1' instead of -'0' because FEN is 1 indexed instead of 0 indexed
                enPassantSquare = (Util.FileToInt(fenParts[3][0]) + (8 * (fenParts[3][1] - '1')));
            }
        }
        public MoveGenerationResult GetMoves()
        {
            MoveGenerationResult result = new MoveGenerationResult();
            List<Move> returnMoves = new List<Move>();
            int[] occupationBoard = new int[8 * 8];
            int[] currentBoard;
            List<int> boardList = new List<int>();
            List<Move> nonCheckingMoves = new List<Move>();
            List<int> myPieces = _positions.Where(e => _whiteToMove == Util.IsWhite(e)).ToList();
            List<int> enemyPieces = _positions.Where(e => _whiteToMove == !Util.IsWhite(e)).ToList();
            bool kingChecked = false;
            int currentKingPosition;
            //this can maybe be offloaded into move()
            foreach (int piece in _positions)
            {
                if (_whiteToMove && Util.IsWhiteKing(piece))
                {
                    currentKingPiece = piece;
                }
                if (!_whiteToMove && Util.IsBlackKing(piece))
                {
                    currentKingPiece = piece;
                }
                occupationBoard[Util.GetPieceOffset(piece)] = piece;
            }
            currentKingPosition = Util.GetPieceOffset(currentKingPiece);

            List<int> occupationList = occupationBoard.ToList();
            Dictionary<int, List<Move>> enemyMovements = new Dictionary<int, List<Models.Move>>();

            List<Move> tempList;
            //see if king is currently in check. If yes and no moves are generated it is checkmate. If no and no moves generated it is stalemate
            foreach (int piece in enemyPieces)
            {
                enemyMovements.Add(piece, MoveGenerator.GenerateMovesForPiece(piece, _theBoard));
                bool checkit = enemyMovements.TryGetValue(piece, out tempList);
                if (checkit && KingCheckFinder.IsKingChecked(currentKingPosition, tempList))
                {
                    kingChecked = true;
                }
            }

            foreach (int piece in myPieces)
            {
                returnMoves.AddRange(MoveGenerator.GenerateMovesForPiece(piece, _theBoard));
            }

            //if I am in check, I can only return moves that resolve it, as well as moves that dont cause check
            foreach (Move m in returnMoves)
            {
                currentBoard = new int[8 * 8];
                bool kingFutureChecked = false;

                Move maybeCapturedEnemy = Move(m);

                //probably less operations if I just maintain the 8x8 board
                foreach (int piece in _positions)
                {
                    int x = Util.GetXForPiece(piece);
                    int y = Util.GetYForPiece(piece);
                    currentBoard[x + (8 * y)] = piece;
                    if (!_whiteToMove && Util.IsWhiteKing(piece))
                    {
                        currentKingPiece = piece;
                    }
                    if (_whiteToMove && Util.IsBlackKing(piece))
                    {
                        currentKingPiece = piece;
                    }
                }

                currentKingPosition = Util.GetPieceOffset(currentKingPiece);
                //its not my turn right now because of move(), so this linq returns my enemies
                foreach (int piece in _positions.Where(e => _whiteToMove == Util.IsWhite(e)))
                {

                    if (KingCheckFinder.IsKingChecked(currentKingPosition, MoveGenerator.GenerateMovesForPiece(piece, _theBoard)))
                    {
                        kingFutureChecked = true;
                        break;
                    }

                }
                Undo();
                if (!kingFutureChecked)
                {
                    nonCheckingMoves.Add(m);
                }

            }
            if (nonCheckingMoves.Count == 0)
            {
                if (kingChecked)
                    result.Endgame = EndgameType.Checkmate;
                else
                    result.Endgame = EndgameType.Stalemate;
            }
            result.Moves = nonCheckingMoves;

            return result;

        }

        public double BoardValue()
        {
            double value = 0;
            //if (_positions.Count == 32)
            //{
            //    return 0;
            //}
            foreach (int piece in _theBoard.Where(e => e > 0))
            {
                char pN = Util.GetPieceName(piece);
                int x = Util.GetXForPiece(piece);
                int y = Util.GetYForPiece(piece);
                switch (pN)
                {
                    case 'P':
                        value += 10 + pawnEvalWhite[y][x]; ;
                        break;
                    case 'p':
                        value -= 10 + pawnEvalBlack[y][x]; ;
                        break;
                    case 'R':
                        value += 50 + rookEvalWhite[y][x];
                        break;
                    case 'r':
                        value -= 50 + rookEvalBlack[y][x];
                        break;
                    case 'B':
                        value += 30 + bishopEvalWhite[y][x];
                        break;
                    case 'b':
                        value -= 30 + bishopEvalBlack[y][x];
                        break;
                    case 'N':
                        value += 30 + knightEval[y][x];
                        break;
                    case 'n':
                        value -= 30 + knightEval[y][x];
                        break;
                    case 'Q':
                        value += 90 + queenEval[y][x];
                        break;
                    case 'q':
                        value -= 90 + queenEval[y][x];
                        break;
                    case 'K':
                        value += 900 + kingEvalWhite[y][x];
                        break;
                    case 'k':
                        value -= 900 + kingEvalBlack[y][x];
                        break;
                    default:
                        break;
                }

                //value += Util.GetPieceValue(piece);
            }
            return value;
        }
        public Move Move(Move m)
        {
            bool isWhiteMoving = Util.IsWhite(m.From);
            int fromOffset = Util.GetPieceOffset(m.From);
            int toOffset = Util.GetPieceOffset(m.To);


            _theBoard[fromOffset] = 0;
            if (_theBoard[toOffset] > 0 && Util.IsWhite(_theBoard[toOffset]) != isWhiteMoving)
            {
                //check for captures and then record in history item, allows for undo
                m.Captured = _theBoard[toOffset];
            }
            _theBoard[toOffset] = m.To;

            _whiteToMove = !_whiteToMove;
            history.Push(m);
            _positions = _theBoard.Where(e => e > 0).ToList();
            return history.Peek();

        }
        public void Undo()
        {
            if (history.Count > 0)
            {
                Move move = history.Pop();
                int fromOffset = Util.GetPieceOffset(move.From);
                int toOffset = Util.GetPieceOffset(move.To);

                _theBoard[fromOffset] = move.From;

                _theBoard[toOffset] = 0;
                if (move.Captured.HasValue)
                {
                    _theBoard[toOffset] = move.Captured.Value;
                }
                _whiteToMove = !_whiteToMove;
                _positions = _theBoard.Where(e => e > 0).ToList();
            }
        }
        public string ToFENString()
        {
            string result = "";

            string[] resultArr = new string[8 * 8];
            foreach (int piece in _theBoard.Where(e => e > 0))
            {
                resultArr[Util.GetPieceOffset(piece)] = PieceTypeFENMap.PieceName(piece).ToString();
            }
            int count = 0;
            StringBuilder sb = new StringBuilder();
            StringBuilder sbRow = new StringBuilder();
            //we have to re-reverse the result for text-box
            int skipGrabber = 0;
            foreach (string p in resultArr.Reverse())
            {
                if (count % 8 == 0 && count > 0)
                {
                    sb.Append("/");
                }
                if (p != null)
                {
                    if (skipGrabber != 0)
                    {
                        sbRow.Append(skipGrabber);
                        skipGrabber = 0;
                    }
                    sbRow.Append(p);
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
                        sbRow.Append(skipGrabber);
                        skipGrabber = 0;
                    }
                }
                if (count % 8 == 0)
                {
                    string ss = new string(sbRow.ToString().Reverse().ToArray());
                    sb.Append(ss);
                    sbRow.Clear();
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
            if (_whiteOOCastle || _whiteOOOCastle || _blackOOCastle || _blackOOOCastle)
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
            }
            else
            {
                sb.Append("-");
            }


            if (enPassantSquare.HasValue)
            {
                sb.Append(" ");
                sb.Append(Util.IntToFile(Util.GetXForPosition((byte)enPassantSquare.Value)));
                sb.Append(Util.GetYForPosition((byte)enPassantSquare.Value) + 1);
            }
            else
            {
                sb.Append(" -");
            }
            return sb.ToString();
        }
        public override string ToString()
        {
            char[][] resultArr = new char[8][];
            for (int i = 0; i < 8; i++)
            {
                resultArr[i] = new char[8];
            }

            int rowCount = 0;
            int rowIndex = 0;
            foreach (int piece in _theBoard.Where(e => e > 0))
            {


                resultArr[Util.GetYForPiece(piece)][Util.GetXForPiece(piece)] = PieceTypeFENMap.PieceName(piece);
                rowCount++;
                if (rowCount % 8 == 0)
                {
                    rowIndex++;
                }

            }
            int count = 0;
            StringBuilder sb = new StringBuilder();
            //we have to re-reverse the result for text-box
            char[] rowText = new char[8];
            resultArr = resultArr.Reverse().ToArray();
            for (int i = 0; i < 8; i++)
            {
                foreach (char p in resultArr[i])
                {
                    rowText[count % 8] = p == 0 ? '-' : p;
                    count++;
                    if (count % 8 == 0)
                    {
                        sb.Append(rowText);
                        sb.Append("\r\n");
                    }
                }
            }
            return sb.ToString();
        }

    }
}
