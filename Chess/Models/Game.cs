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
        double[] whitePawnEval = new double[64] { 0, 0, 0, 0, 0, 0, 0, 0, 5, 10, 10, -20, -20, 10, 10, 5, 5, -5, -10, 0, 0, -10, -5, 5, 0, 0, 0, 20, 20, 0, 0, 0, 5, 5, 10, 25, 25, 10, 5, 5, 10, 10, 20, 30, 30, 20, 10, 10, 50, 50, 50, 50, 50, 50, 50, 50, 0, 0, 0, 0, 0, 0, 0, 0 };

        double[] blackPawnEval = new double[64] { 0, 0, 0, 0, 0, 0, 0, 0, 50, 50, 50, 50, 50, 50, 50, 50, 10, 10, 20, 30, 30, 20, 10, 10, 5, 5, 10, 25, 25, 10, 5, 5, 0, 0, 0, 20, 20, 0, 0, 0, 5, -5, -10, 0, 0, -10, -5, 5, 5, 10, 10, -20, -20, 10, 10, 5, 0, 0, 0, 0, 0, 0, 0, 0 };

        double[] whiteBishopEval = new double[64] { -20, -10, -10, -10, -10, -10, -10, -20, -10, 5, 0, 0, 0, 0, 5, -10, -10, 10, 10, 10, 10, 10, 10, -10, -10, 0, 10, 10, 10, 10, 0, -10, -10, 5, 5, 10, 10, 5, 5, -10, -10, 5, 5, 10, 10, 5, 5, -10, -10, 0, 0, 0, 0, 0, 0, -10, -20, -10, -10, -10, -10, -10, -10, -20 };

        double[] blackBishopEval = new double[64] { -20, -10, -10, -10, -10, -10, -10, -20, -10, 0, 0, 0, 0, 0, 0, -10, -10, 0, 5, 10, 10, 5, 0, -10, -10, 5, 5, 10, 10, 5, 5, -10, -10, 0, 10, 10, 10, 10, 0, -10, -10, 10, 10, 10, 10, 10, 10, -10, -10, 5, 0, 0, 0, 0, 5, -10, -20, -10, -10, -10, -10, -10, -10, -20 };

        double[] whiteRookEval = new double[64] { 0, 0, 0, 5, 5, 0, 0, 0, -5, 0, 0, 0, 0, 0, 0, -5, -5, 0, 0, 0, 0, 0, 0, -5, -5, 0, 0, 0, 0, 0, 0, -5, -5, 0, 0, 0, 0, 0, 0, -5, -5, 0, 0, 0, 0, 0, 0, -5, 5, 10, 10, 10, 10, 10, 10, 5, 0, 0, 0, 0, 0, 0, 0, 0 };

        double[] blackRookEval = new double[64] { 0, 0, 0, 0, 0, 0, 0, 0, 5, 10, 10, 10, 10, 10, 10, 5, -5, 0, 0, 0, 0, 0, 0, -5, -5, 0, 0, 0, 0, 0, 0, -5, -5, 0, 0, 0, 0, 0, 0, -5, -5, 0, 0, 0, 0, 0, 0, -5, -5, 0, 0, 0, 0, 0, 0, -5, 0, 0, 0, 5, 5, 0, 0, 0 };

        double[] knightEval = new double[64] { -50, -40, -30, -30, -30, -30, -40, -50, -40, -20, 0, 5, 5, 0, -20, -40, -30, 5, 10, 15, 15, 10, 5, -30, -30, 0, 15, 20, 20, 15, 0, -30, -30, 5, 15, 20, 20, 15, 5, -30, -30, 0, 10, 15, 15, 10, 0, -30, -40, -20, 0, 0, 0, 0, -20, -40, -50, -40, -30, -30, -30, -30, -40, -50 };

        double[] whiteQueenEval = new double[64] {
            -20, -10, -10, -5, -5, -10, -10, -20,
            -10, 0, 5, 0, 0, 0, 0, -10,
            -10, 5, 5, 5, 5, 5, 0, -10,
            0, 0, 5, 5, 5, 5, 0, -5,
            -5, 0, 5, 5, 5, 5, 0, -5,
            -10, 0, 5, 5, 5, 5, 0, -10,
            -10, 0, 0, 0, 0, 0, 0, -10,
            -20, -10, -10, -5, -5, -10, -10, -20 };

        double[] blackQueenEval = new double[64] {
            -20, -10, -10, -5, -5, -10, -10, -20,
            -10, 0, 0, 0, 0, 0, 0, -10,
            -10, 0, 5, 5, 5, 5, 0, -10,
            -5, 0, 5, 5, 5, 5, 0, -5,
            0, 0, 5, 5, 5, 5, 0, -5,
            -10, 5, 5, 5, 5, 5, 0, -10,
            -10, 0, 5, 0, 0, 0, 0, -10,
            -20, -10, -10, -5, -5, -10, -10, -20 };

        double[] whiteKingEval = new double[64] { 20, 30, 10, 0, 0, 10, 30, 20, 20, 20, 0, 0, 0, 0, 20, 20, -10, -20, -20, -20, -20, -20, -20, -10, -20, -30, -30, -40, -40, -30, -30, -20, -30, -40, -40, -50, -50, -40, -40, -30, -30, -40, -40, -50, -50, -40, -40, -30, -30, -40, -40, -50, -50, -40, -40, -30, -30, -40, -40, -50, -50, -40, -40, -30 };

        double[] blackKingEval = new double[64] { -30, -40, -40, -50, -50, -40, -40, -30, -30, -40, -40, -50, -50, -40, -40, -30, -30, -40, -40, -50, -50, -40, -40, -30, -30, -40, -40, -50, -50, -40, -40, -30, -20, -30, -30, -40, -40, -30, -30, -20, -10, -20, -20, -20, -20, -20, -20, -10, 20, 20, 0, 0, 0, 0, 20, 20, 20, 30, 10, 0, 0, 10, 30, 20 };

        private int[] _theBoard;
        private bool _whiteOOCastle = true;
        private bool _whiteOOOCastle = true;
        private bool _blackOOCastle = true;
        private bool _blackOOOCastle = true;
        List<int> whiteOOCastleAttackedSquares = new List<int>() { 5, 6 };
        List<int> whiteOOOCastleAttackedSquares = new List<int>() { 2, 3 };
        List<int> blackOOCastleAttackedSquares = new List<int>() { 61, 62 };
        List<int> blackOOOCastleAttackedSquares = new List<int>() { 58, 59 };
        List<int> whiteOOOCastleOccupiedSquares = new List<int>() {1, 2, 3 };
        List<int> blackOOOCastleOccupiedSquares = new List<int>() { 57, 58, 59 };
        private bool _whiteToMove = true;
        private int? enPassantSquare;
        int whiteKing = 0, blackKing = 0, currentKingPiece;
        public Stack<Move> history;
        Dictionary<int, List<Move>> nextMoves = new Dictionary<int, List<Move>>();
        List<Move> actualNextMoves = new List<Move>();
        ulong one = 1;
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
        }
        public Game(string fen)
        {
            _theBoard = new int[64];
            history = new Stack<Move>();

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
                        if (char.IsNumber(row[x]))
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
            foreach (int p in _theBoard)
            {
                if (Util.IsWhiteKing(p))
                {
                    whiteKing = p;
                }
                else if (Util.IsBlackKing(p))
                {
                    blackKing = p;
                }
            }
        }

        public MoveGenerationResult GetMoves()
        {
            MoveGenerationResult result = new MoveGenerationResult();
            List<Move> myGeneratedMoves = new List<Move>();
            List<Move> nonCheckingMoves = new List<Move>();
            List<ulong> enemyPseudoMoves = new List<ulong>();
            List<int> threateningPieces = new List<int>();
            List<int> threateningPiecesNonKingMove = new List<int>();
            IEnumerable<int> positions = _theBoard.Where(e => e > 0);
            List<int> myPieces = positions.Where(e => _whiteToMove == Util.IsWhite(e)).ToList();
            List<int> enemyPieces = positions.Where(e => _whiteToMove == !Util.IsWhite(e)).ToList();
            bool kingChecked = false;
            int currentKingPosition;
            int opponentKingPiece;
            double materialScore = Material();
            if (_whiteToMove)
            {
                currentKingPiece = whiteKing;
            }
            else
            {
                currentKingPiece = blackKing;
            }
            if (!_whiteToMove)
            {
                opponentKingPiece = whiteKing;
            }
            else
            {
                opponentKingPiece = blackKing;
            }
            currentKingPosition = Util.GetPieceOffset(currentKingPiece);

            Dictionary<int, List<Move>> enemyMovements = new Dictionary<int, List<Models.Move>>();

            List<Move> pieceMoves;
            //see if king is currently in check. If yes and no moves are generated it is checkmate. If no and no moves generated it is stalemate
            bool allowOOOCastle=true, allowOOCastle=true;
            foreach (int piece in enemyPieces)
            {
                pieceMoves = MoveGenerator.GenerateMovesForPiece(piece, _theBoard);
                if (Util.IsSquareAttacked(currentKingPosition, pieceMoves))
                {
                    kingChecked = true;
                }
                else
                {
                    if (_whiteToMove)
                    {
                        if(Util.AreSquaresAttacked(whiteOOCastleAttackedSquares, pieceMoves))
                        {
                            allowOOCastle = false;
                        }
                        if (Util.AreSquaresAttacked(whiteOOOCastleAttackedSquares, pieceMoves))
                        {
                            allowOOOCastle = false;
                        }
                    }
                    else
                    {
                        if (Util.AreSquaresAttacked(blackOOCastleAttackedSquares, pieceMoves))
                        {
                            allowOOCastle = false;
                        }
                        if (Util.AreSquaresAttacked(blackOOOCastleAttackedSquares, pieceMoves))
                        {
                            allowOOOCastle = false;
                        }
                    }
                }
                //get all targeted enemy squares without regard for blocking squares. Used to trim check verification step
                enemyPseudoMoves.Add(MoveGenerator.PseudomoveBitboard(piece, _theBoard));
            }
            if (!kingChecked)
            {
                if (_whiteToMove)
                {
                    if (_whiteOOCastle && allowOOCastle && Util.AreSquaresEmpty(whiteOOCastleAttackedSquares, _theBoard))
                    {
                        nonCheckingMoves.Add(new Move() { From = whiteKing, To = whiteKing + 2, CastleRookFrom = _theBoard[7], CastleRookTo = _theBoard[7] - 2, removesOO = true });
                    }
                    if (_whiteOOOCastle && allowOOOCastle && Util.AreSquaresEmpty(whiteOOOCastleOccupiedSquares, _theBoard))
                    {
                        nonCheckingMoves.Add(new Move() { From = whiteKing, To = whiteKing - 2, CastleRookFrom = _theBoard[0], CastleRookTo = _theBoard[0] + 3, removesOOO = true });
                    }
                }
                else
                {
                    if (_blackOOCastle && allowOOOCastle && Util.AreSquaresEmpty(blackOOCastleAttackedSquares, _theBoard))
                    {
                        nonCheckingMoves.Add(new Move() { From = blackKing, To = blackKing + 2, CastleRookFrom = _theBoard[63], CastleRookTo = _theBoard[63] - 2, removesOO = true });
                    }
                    if (_blackOOOCastle && allowOOOCastle && Util.AreSquaresEmpty(blackOOOCastleOccupiedSquares, _theBoard))
                    {
                        nonCheckingMoves.Add(new Move() { From = blackKing, To = blackKing - 2, CastleRookFrom = _theBoard[56], CastleRookTo = _theBoard[56] + 3, removesOOO = true });
                    }
                }

            }
            //generate legal moves for each of my pieces
            foreach (int piece in myPieces)
            {
                myGeneratedMoves.AddRange(MoveGenerator.GenerateMovesForPiece(piece, _theBoard));
            }


            ulong movedKingPositions = KingMoveGenerator.PseudomoveBitboard(currentKingPiece);
            ulong unmovedKingPosition = (one << (currentKingPosition));
            foreach (int piece in _theBoard.Where(e => e > 0 && _whiteToMove != Util.IsWhite(e)))
            {
                //find out if the move caused the king to be in check
                if ((movedKingPositions & MoveGenerator.PseudomoveBitboard(piece, _theBoard)) > 0)
                {
                    threateningPieces.Add(piece);
                }
                if ((unmovedKingPosition & MoveGenerator.PseudomoveBitboard(piece, _theBoard)) > 0)
                {
                    threateningPiecesNonKingMove.Add(piece);
                }
            }

            foreach (Move m in myGeneratedMoves)
            {
                bool kingFutureChecked = false;         

                Move maybeCapturedEnemy = Move(m, true);

                m.MaterialScore = materialScore + (maybeCapturedEnemy.Captured.HasValue ? Util.GetPieceValue(maybeCapturedEnemy.Captured.Value) : 0);

                if (!_whiteToMove)
                {
                    currentKingPiece = whiteKing;
                }
                else
                {
                    currentKingPiece = blackKing;
                }
                currentKingPosition = Util.GetPieceOffset(currentKingPiece);
                if (Util.IsKing(m.From))
                {
                    foreach (int piece in threateningPieces.Where(e => _theBoard.Contains(e)))
                    {
                        //find out if the move caused the king to be in check
                        if (Util.IsSquareAttacked(currentKingPosition, MoveGenerator.GenerateMovesForPiece(piece, _theBoard)))
                        {
                            kingFutureChecked = true;
                        }
                    }
                }else
                {
                    foreach (int piece in threateningPiecesNonKingMove.Where(e => _theBoard.Contains(e)))
                    {
                        //find out if the move caused the king to be in check
                        if (Util.IsSquareAttacked(currentKingPosition, MoveGenerator.GenerateMovesForPiece(piece, _theBoard)))
                        {
                            kingFutureChecked = true;
                        }
                    }
                }

                Undo();
                if (!kingFutureChecked)
                {
                    nonCheckingMoves.Add(m);
                }
            }

            result.Moves = nonCheckingMoves;
            if (nonCheckingMoves.Count == 0)
            {
                if (kingChecked)
                    result.Endgame = EndgameType.Checkmate;
                else
                    result.Endgame = EndgameType.Stalemate;
            }
            return result;
        }

        public Move Move(Move m, bool keepNextMoves = false)
        {
            bool isWhiteMoving = Util.IsWhite(m.From);
            int fromOffset = Util.GetPieceOffset(m.From);
            int toOffset = Util.GetPieceOffset(m.To);
            int currentToPiece = _theBoard[toOffset];

            //remove the moving piece from previous square
            _theBoard[fromOffset] = 0;
            if (m.CastleRookFrom > 0)
            {
                _theBoard[Util.GetPieceOffset(m.CastleRookFrom)] = 0;
                _theBoard[Util.GetPieceOffset(m.CastleRookTo)] = m.CastleRookTo;
            }
            if (currentToPiece > 0 && Util.IsWhite(currentToPiece) != isWhiteMoving)
            {
                //check for captures and then record in history item, allows for undo
                m.Captured = currentToPiece;
            }
            //move piece to "to" position
            _theBoard[toOffset] = m.To;

            //maintain the kings on move so they dont need to be looked up during move generation
            if (Util.IsKing(m.To))
            {
                if (m.CastleRookFrom > 0)
                {
                    if (_whiteToMove)
                    {
                        if (m.removesOO)
                            _whiteOOCastle = false;
                        if (m.removesOOO)
                            _whiteOOOCastle = false;
                    }
                    else
                    {
                        if (m.removesOO)
                            _blackOOCastle = false;
                        if (m.removesOOO)
                            _blackOOOCastle = false;
                    }
                    m.removesOOO = true;
                }
                else
                {
                    if (_whiteToMove)
                    {
                        whiteKing = m.To;
                        _whiteOOCastle = false;
                        _whiteOOOCastle = false;
                    }
                    else
                    {
                        blackKing = m.To;
                        _blackOOCastle = false;
                        _blackOOOCastle = false;
                    }
                    m.removesOO = true;
                    m.removesOOO = true;
                }
            }


            _whiteToMove = !_whiteToMove;
            history.Push(m);
            if (!keepNextMoves)
            {
                if (!nextMoves.TryGetValue((m.From << 9) + m.To, out actualNextMoves))
                {
                    actualNextMoves = new List<Move>();
                }
                nextMoves.Clear();
            }
            //return the move so the caller can know if capture happened
            return history.Peek();

        }
        public void Undo()
        {
            if (history.Count > 0)
            {
                Move m = history.Pop();
                int fromOffset = Util.GetPieceOffset(m.From);
                int toOffset = Util.GetPieceOffset(m.To);

                //move the piece back to its previous position, which should always be 0
                _theBoard[fromOffset] = m.From;

                if (m.CastleRookFrom > 0)
                {
                    _theBoard[Util.GetPieceOffset(m.CastleRookFrom)] = m.CastleRookFrom;
                    _theBoard[Util.GetPieceOffset(m.CastleRookTo)] = 0;
                    if (_whiteToMove)
                    {
                        if (m.removesOO)
                            _blackOOCastle=true;
                        if (m.removesOOO)
                            _blackOOOCastle = true;
                    }else
                    {
                        if (m.removesOO)
                            _whiteOOCastle = true;
                        if (m.removesOOO)
                            _whiteOOOCastle = true;
                    }
                }
                //set the king reference back to previous value
                if (Util.IsKing(m.From))
                {
                    if (Util.IsWhite(m.From))
                        whiteKing = m.From;
                    else
                        blackKing = m.From;
                }
                //clear the square it had moved into
                _theBoard[toOffset] = 0;
                //and put back the piece it captured if any
                if (m.Captured.HasValue)
                {
                    _theBoard[toOffset] = m.Captured.Value;
                }
                _whiteToMove = !_whiteToMove;
            }
        }
        public double BoardValue()
        {
            double value = 0;

            foreach (int piece in _theBoard.Where(e => e > 0))
            {
                char pN = Util.GetPieceName(piece);
                int o = Util.GetPieceOffset(piece);
                switch (pN)
                {
                    case 'P':
                        value += 100 + whitePawnEval[o]; ;
                        break;
                    case 'p':
                        value -= 100 + blackPawnEval[o]; ;
                        break;
                    case 'R':
                        value += 500 + whiteRookEval[o];
                        break;
                    case 'r':
                        value -= 500 + blackRookEval[o];
                        break;
                    case 'B':
                        value += 300 + whiteBishopEval[o];
                        break;
                    case 'b':
                        value -= 300 + blackBishopEval[o];
                        break;
                    case 'N':
                        value += 300 + knightEval[o];
                        break;
                    case 'n':
                        value -= 300 + knightEval[o];
                        break;
                    case 'Q':
                        value += 900 + whiteQueenEval[o];
                        break;
                    case 'q':
                        value -= 900 + blackQueenEval[o];
                        break;
                    case 'K':
                        value += 9000 + whiteKingEval[o];
                        break;
                    case 'k':
                        value -= 9000 + blackKingEval[o];
                        break;
                    default:
                        break;
                }
            }
            return value;
        }
        public double Material()
        {
            double value = 0;
            foreach (int piece in _theBoard.Where(e => e > 0))
            {
                char pN = Util.GetPieceName(piece);

                switch (pN)
                {
                    case 'P':
                        value += 100;
                        break;
                    case 'p':
                        value -= 100;
                        break;
                    case 'R':
                        value += 500;
                        break;
                    case 'r':
                        value -= 500;
                        break;
                    case 'B':
                        value += 300;
                        break;
                    case 'b':
                        value -= 300;
                        break;
                    case 'N':
                        value += 300;
                        break;
                    case 'n':
                        value -= 300;
                        break;
                    case 'Q':
                        value += 900;
                        break;
                    case 'q':
                        value -= 900;
                        break;
                    case 'K':
                        value += 9000;
                        break;
                    case 'k':
                        value -= 9000;
                        break;
                    default:
                        break;
                }
            }
            return value;
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
