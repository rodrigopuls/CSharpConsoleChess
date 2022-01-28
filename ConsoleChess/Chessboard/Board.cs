using ConsoleChess.Chessboard;

namespace Chessboard
{
    public class Board
    {
        public int Lines { get; set; }
        public int Columns { get; set; }
        private Piece[,] Pieces;

        public Board(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            Pieces = new Piece[lines, columns];
        }

        public Piece Piece(int line, int column)
        {
            return Pieces[line, column];
        }

        public Piece Piece(Position position)
        {
            return Pieces[position.Line, position.Column];
        }

        public bool HasPieceInPosition(Position position)
        {
            ValidatePosition(position);
            return Piece(position) != null;
        }

        public void InsertPiece(Piece piece, Position position)
        {
            if (HasPieceInPosition(position))
                throw new BoardException("Position is already occupied!");

            Pieces[position.Line, position.Column] = piece;
            piece.Position = position;
        }

        public Piece RemovePiece(Position position)
        {
            if (Piece(position) == null)
                return null;

            Piece aux = Piece(position);
            aux.Position = null;

            Pieces[position.Line, position.Column] = null;

            return aux;
        }

        public bool IsValidPosition(Position position)
        {
            if (position.Line < 0 || position.Line >= Lines || position.Column < 0 || position.Column >= Columns)
                return false;

            return true;
        }

        public void ValidatePosition(Position position)
        {
            if (!IsValidPosition(position))
                throw new BoardException("Invalid position!");
        }
    }
}