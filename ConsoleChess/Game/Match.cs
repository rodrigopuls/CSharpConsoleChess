using System.Collections.Generic;
using Chessboard;
using ConsoleChess.Chessboard;

namespace Game
{
    public class Match
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool GameOver { get; private set; }
        private HashSet<Piece> _boardPieces;
        private HashSet<Piece> _capturedPieces;
        public bool Checkmate { get; set; }
        public Piece VunerableToEnPassantMove { get; private set; }

        public Match()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            GameOver = false;
            _boardPieces = new HashSet<Piece>();
            _capturedPieces = new HashSet<Piece>();
            Checkmate = false;
            VunerableToEnPassantMove = null;

            InsertPieces();
        }

        private Piece MovePiece(Position initial, Position final)
        {
            Piece piece = Board.RemovePiece(initial);

            piece.IncrementMovementQuantity();

            Piece capturedPiece = Board.RemovePiece(final);

            Board.InsertPiece(piece, final);

            if (capturedPiece != null)
                _capturedPieces.Add(capturedPiece);

            // special move king-side castling
            if (piece is King && final.Column == initial.Column + 2)
            {
                Position rookInitial = new Position(initial.Line, initial.Column + 3);
                Position rookFinal = new Position(initial.Line, initial.Column + 1);

                Piece rook = Board.RemovePiece(rookInitial);
                rook.IncrementMovementQuantity();
                Board.InsertPiece(rook, rookFinal);
            }

            // special move queen-side castling
            if (piece is King && final.Column == initial.Column - 2)
            {
                Position rookInitial = new Position(initial.Line, initial.Column - 4);
                Position rookFinal = new Position(initial.Line, initial.Column - 1);

                Piece rook = Board.RemovePiece(rookInitial);
                rook.IncrementMovementQuantity();
                Board.InsertPiece(rook, rookFinal);
            }

            // special move en passant
            if (piece is Pawn)
            {
                if (initial.Column != final.Column && capturedPiece == null)
                {
                    Position capturedPawnPosition = piece.Color == Color.White
                                            ? new Position(final.Line + 1, final.Column)
                                            : new Position(final.Line - 1, final.Column);

                    capturedPiece = Board.RemovePiece(capturedPawnPosition);
                    _capturedPieces.Add(capturedPiece);
                }
            }

            return capturedPiece;
        }

        private void UndoMovePiece(Position initial, Position final, Piece piece)
        {
            Piece capturedPiece = Board.RemovePiece(final);
            capturedPiece.DecrementMovementQuantity();

            if (piece != null)
            {
                Board.InsertPiece(piece, final);
                _capturedPieces.Remove(piece);
            }

            Board.InsertPiece(capturedPiece, initial);

            // special move king-side castling
            if (piece is King && final.Column == initial.Column + 2)
            {
                Position rookInitial = new Position(initial.Line, initial.Column + 3);
                Position rookFinal = new Position(initial.Line, initial.Column + 1);

                Piece rook = Board.RemovePiece(rookFinal);
                rook.DecrementMovementQuantity();
                Board.InsertPiece(rook, rookInitial);
            }

            // special move queen-side castling
            if (piece is King && final.Column == initial.Column - 2)
            {
                Position rookInitial = new Position(initial.Line, initial.Column - 4);
                Position rookFinal = new Position(initial.Line, initial.Column - 1);

                Piece rook = Board.RemovePiece(rookFinal);
                rook.DecrementMovementQuantity();
                Board.InsertPiece(rook, rookInitial);
            }

            // special move en passant
            if (piece is Pawn)
            {
                if (initial.Column != final.Column && capturedPiece == VunerableToEnPassantMove)
                {
                    Piece capturedPawn = Board.RemovePiece(final);
                    Position capturedPawnPosition = piece.Color == Color.White
                        ? new Position(3, final.Column)
                        : new Position(4, final.Column);

                    Board.InsertPiece(capturedPawn, capturedPawnPosition);
                }
            }
        }

        public void PlayTurn(Position initial, Position final)
        {
            Piece capturedPiece = MovePiece(initial, final);

            if (IsKingInCheckmate(CurrentPlayer))
            {
                UndoMovePiece(initial, final, capturedPiece);
                throw new BoardException("You can't put yourself in checkmate!");
            }

            Piece piece = Board.Piece(final);

            // special move promotion
            if (piece is Pawn)
            {
                if ((piece.Color == Color.White && final.Line == 0) || (piece.Color == Color.Black && final.Line == 7))
                {
                    piece = Board.RemovePiece(final);
                    _boardPieces.Remove(piece);

                    Piece queen = new Queen(Board, piece.Color);

                    Board.InsertPiece(queen, final);
                    _boardPieces.Add(queen);
                }
            }

            Checkmate = IsKingInCheckmate(Opponnent(CurrentPlayer));

            if (AnyCheckmateEscape(Opponnent(CurrentPlayer)))
            {
                GameOver = true;
            }
            else
            {
                Turn++;
                ChangePlayerTurn();
            }

            // special move en passant
            if (piece is Knight && (final.Line == initial.Line - 2 || final.Line == initial.Line + 2))
            {
                VunerableToEnPassantMove = piece;
            }
            else
            {
                VunerableToEnPassantMove = null;
            }

        }

        public void ValidateInitialPosition(Position position)
        {
            if (Board.Piece(position) == null)
                throw new BoardException("Nothing selected! Press any key to continue.");

            if (CurrentPlayer != Board.Piece(position).Color)
                throw new BoardException("Invalid piece color! Press any key to continue.");

            if (!Board.Piece(position).HasPossibleMoves())
                throw new BoardException("No possible moves! Press any key to continue.");
        }

        public void ValidateFinalPosition(Position initial, Position final)
        {
            if (!Board.Piece(initial).IsPossibleMoveTo(final))
                throw new BoardException("Invalid final position! Press any key to continue.");
        }

        private void ChangePlayerTurn()
        {
            CurrentPlayer = CurrentPlayer == Color.White ? Color.Black : Color.White;
        }

        public HashSet<Piece> Get_capturedPiecesByColor(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (var piece in _capturedPieces)
            {
                if (piece.Color == color)
                    aux.Add(piece);
            }

            return aux;
        }

        public HashSet<Piece> Get_boardPiecesByColor(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (var piece in _boardPieces)
            {
                if (piece.Color == color)
                    aux.Add(piece);
            }
            aux.ExceptWith(Get_capturedPiecesByColor(color));
            return aux;
        }

        private Color Opponnent(Color color)
        {
            if (color == Color.White)
                return Color.Black;
            else
                return Color.White;
        }

        private Piece GetKingByColor(Color color)
        {
            foreach (var piece in Get_boardPiecesByColor(color))
            {
                if (piece is King)
                    return piece;
            }

            return null;
        }

        public bool IsKingInCheckmate(Color color)
        {
            Piece king = GetKingByColor(color);

            if (king == null)
                throw new BoardException("Something went wrong!");

            foreach (var piece in Get_boardPiecesByColor(Opponnent(color)))
            {
                bool[,] possibleMoves = piece.GetAllPossibleMoves();
                if (possibleMoves[king.Position.Line, king.Position.Column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool AnyCheckmateEscape(Color color)
        {
            if (IsKingInCheckmate(color))
            {
                return false;
            }

            foreach (Piece piece in Get_boardPiecesByColor(color))
            {
                bool[,] possibleMoves = piece.GetAllPossibleMoves();
                for (int l = 0; l < Board.Lines; l++)
                {
                    for (int c = 0; c < Board.Columns; c++)
                    {
                        if (possibleMoves[l, c])
                        {
                            Position initial = piece.Position;
                            Position final = new Position(l, c);

                            Piece capturedPiece = MovePiece(initial, final);

                            bool checkmate = IsKingInCheckmate(color);

                            UndoMovePiece(initial, final, capturedPiece);
                            if (!checkmate)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        public void InsertNewPiece(char column, int line, Piece piece)
        {
            Board.InsertPiece(piece, new BoardPosition(column, line).ToPosition());
            _boardPieces.Add(piece);
        }

        private void InsertPieces()
        {
            InsertNewPiece('a', 1, new Rook(Board, Color.White));
            InsertNewPiece('b', 1, new Knight(Board, Color.White));
            InsertNewPiece('c', 1, new Bishop(Board, Color.White));
            InsertNewPiece('d', 1, new Queen(Board, Color.White));
            InsertNewPiece('e', 1, new King(Board, Color.White, this));
            InsertNewPiece('f', 1, new Bishop(Board, Color.White));
            InsertNewPiece('g', 1, new Knight(Board, Color.White));
            InsertNewPiece('h', 1, new Rook(Board, Color.White));
            InsertNewPiece('a', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('b', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('c', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('d', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('e', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('f', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('g', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('h', 2, new Pawn(Board, Color.White, this));

            InsertNewPiece('a', 8, new Rook(Board, Color.Black));
            InsertNewPiece('b', 8, new Knight(Board, Color.Black));
            InsertNewPiece('c', 8, new Bishop(Board, Color.Black));
            InsertNewPiece('d', 8, new Queen(Board, Color.Black));
            InsertNewPiece('e', 8, new King(Board, Color.Black, this));
            InsertNewPiece('f', 8, new Bishop(Board, Color.Black));
            InsertNewPiece('g', 8, new Knight(Board, Color.Black));
            InsertNewPiece('h', 8, new Rook(Board, Color.Black));
            InsertNewPiece('a', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('b', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('c', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('d', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('e', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('f', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('g', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('h', 7, new Pawn(Board, Color.Black, this));
        }
    }
}