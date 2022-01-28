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
        public bool Ended { get; private set; }
        private HashSet<Piece> BoardPieces;
        private HashSet<Piece> CapturedPieces;

        public Match()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Ended = false;

            BoardPieces = new HashSet<Piece>();
            CapturedPieces = new HashSet<Piece>();

            InsertPieces();
        }

        private void MovePiece(Position initial, Position final)
        {
            Piece piece = Board.RemovePiece(initial);

            piece.IncrementMovementQuantity();

            Piece capturedPiece = Board.RemovePiece(final);

            Board.InsertPiece(piece, final);

            if (capturedPiece != null)
                CapturedPieces.Add(capturedPiece);
        }

        public void PlayTurn(Position initial, Position final)
        {
            MovePiece(initial, final);
            Turn++;
            ChangePlayerTurn();

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

        public HashSet<Piece> CapturedPiecesByColor(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach(var piece in CapturedPieces)
            {
                if(piece.Color == color)
                    aux.Add(piece);
            }

            return aux;
        }

        public HashSet<Piece> BoardPiecesByColor(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach(var piece in BoardPieces)
            {
                if(piece.Color == color)
                    aux.Add(piece);
            }
            aux.ExceptWith(CapturedPiecesByColor(color));
            return aux;
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