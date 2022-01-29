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
        private HashSet<Piece> BoardPieces;
        private HashSet<Piece> CapturedPieces;
        public bool Checkmate { get; set; }

        public Match()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            GameOver = false;
            BoardPieces = new HashSet<Piece>();
            CapturedPieces = new HashSet<Piece>();
            Checkmate = false;

            InsertPieces();
        }

        private Piece MovePiece(Position initial, Position final)
        {
            Piece piece = Board.RemovePiece(initial);

            piece.IncrementMovementQuantity();

            Piece capturedPiece = Board.RemovePiece(final);

            Board.InsertPiece(piece, final);

            if (capturedPiece != null)
                CapturedPieces.Add(capturedPiece);

            return capturedPiece;
        }

        private void UndoMovePiece(Position initial, Position final, Piece piece)
        {
            Piece removedPiece = Board.RemovePiece(final);
            removedPiece.DecrementMovementQuantity();
            if (piece != null)
            {
                Board.InsertPiece(piece, final);
                CapturedPieces.Remove(piece);
            }
            Board.InsertPiece(removedPiece, initial);
        }

        public void PlayTurn(Position initial, Position final)
        {
            Piece capturedPiece = MovePiece(initial, final);

            if (IsKingInCheckmate(CurrentPlayer))
            {
                UndoMovePiece(initial, final, capturedPiece);
                throw new BoardException("You can't put yourself in checkmate!");
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
            if (!Board.Piece(initial).CanMoveTo(final))
                throw new BoardException("Invalid final position! Press any key to continue.");
        }

        private void ChangePlayerTurn()
        {
            CurrentPlayer = CurrentPlayer == Color.White ? Color.Black : Color.White;
        }

        public HashSet<Piece> GetCapturedPiecesByColor(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (var piece in CapturedPieces)
            {
                if (piece.Color == color)
                    aux.Add(piece);
            }

            return aux;
        }

        public HashSet<Piece> GetBoardPiecesByColor(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (var piece in BoardPieces)
            {
                if (piece.Color == color)
                    aux.Add(piece);
            }
            aux.ExceptWith(GetCapturedPiecesByColor(color));
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
            foreach (var piece in GetBoardPiecesByColor(color))
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

            foreach (var piece in GetBoardPiecesByColor(Opponnent(color)))
            {
                bool[,] possibleMoves = piece.PossibleMoves();
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

            foreach (Piece piece in GetBoardPiecesByColor(color))
            {
                bool[,] possibleMoves = piece.PossibleMoves();
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
            BoardPieces.Add(piece);
        }

        private void InsertPieces()
        {
            InsertNewPiece('a', 1, new Rook(Board, Color.White));
            InsertNewPiece('h', 1, new Rook(Board, Color.White));
            InsertNewPiece('e', 1, new King(Board, Color.White));


            InsertNewPiece('a', 8, new Rook(Board, Color.Black));
            InsertNewPiece('h', 8, new Rook(Board, Color.Black));
            InsertNewPiece('e', 8, new King(Board, Color.Black));
        }
    }
}