using Chessboard;

namespace Game
{
    public class Knight : Piece
    {
        public Knight(Board board, Color color) : base(board, color)
        {

        }

        public override string ToString()
        {
            return "H";
        }

        private bool CanMove(Position possiblePosition)
        {
            Piece piece = Board.Piece(possiblePosition);
            return piece == null || piece.Color != Color;
        }

        public override bool[,] GetAllPossibleMoves()
        {
            bool[,] matrix = new bool[Board.Lines, Board.Columns];
            Position possiblePosition = new Position(0, 0);

            possiblePosition.UpdateValues(Position.Line - 1, Position.Column - 2);
            if (Board.IsValidPosition(possiblePosition) && CanMove(possiblePosition))
            {
                matrix[possiblePosition.Line, possiblePosition.Column] = true;
            }

            possiblePosition.UpdateValues(Position.Line - 2, Position.Column - 1);
            if (Board.IsValidPosition(possiblePosition) && CanMove(possiblePosition))
            {
                matrix[possiblePosition.Line, possiblePosition.Column] = true;
            }

            possiblePosition.UpdateValues(Position.Line - 2, Position.Column + 1);
            if (Board.IsValidPosition(possiblePosition) && CanMove(possiblePosition))
            {
                matrix[possiblePosition.Line, possiblePosition.Column] = true;
            }

            possiblePosition.UpdateValues(Position.Line - 1, Position.Column + 2);
            if (Board.IsValidPosition(possiblePosition) && CanMove(possiblePosition))
            {
                matrix[possiblePosition.Line, possiblePosition.Column] = true;
            }

            possiblePosition.UpdateValues(Position.Line + 1, Position.Column + 2);
            if (Board.IsValidPosition(possiblePosition) && CanMove(possiblePosition))
            {
                matrix[possiblePosition.Line, possiblePosition.Column] = true;
            }

            possiblePosition.UpdateValues(Position.Line + 2, Position.Column + 1);
            if (Board.IsValidPosition(possiblePosition) && CanMove(possiblePosition))
            {
                matrix[possiblePosition.Line, possiblePosition.Column] = true;
            }

            possiblePosition.UpdateValues(Position.Line + 2, Position.Column - 1);
            if (Board.IsValidPosition(possiblePosition) && CanMove(possiblePosition))
            {
                matrix[possiblePosition.Line, possiblePosition.Column] = true;
            }

            possiblePosition.UpdateValues(Position.Line + 1, Position.Column - 2);
            if (Board.IsValidPosition(possiblePosition) && CanMove(possiblePosition))
            {
                matrix[possiblePosition.Line, possiblePosition.Column] = true;
            }

            return matrix;
        }

    }
}