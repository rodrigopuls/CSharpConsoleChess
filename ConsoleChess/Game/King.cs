using Chessboard;

namespace Game
{
    public class King : Piece
    {
        public King(Board board, Color color) : base(board, color)
        {

        }

        public override string ToString()
        {
            return "K";
        }

        private bool CanMove(Position possiblePosition)
        {
            Piece piece = Board.Piece(possiblePosition);
            return piece == null || piece.Color != Color;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] matrix = new bool[Board.Lines, Board.Columns];
            Position possiblePosition = new Position(0,0);

            // north
            possiblePosition.UpdateValues(Position.Line - 1, Position.Column);
            if (Board.IsValidPosition(possiblePosition) && CanMove(possiblePosition))
            {
                matrix[possiblePosition.Line, possiblePosition.Column] = true;
            }

            // northeast
            possiblePosition.UpdateValues(Position.Line - 1, Position.Column + 1);
            if (Board.IsValidPosition(possiblePosition) && CanMove(possiblePosition))
            {
                matrix[possiblePosition.Line, possiblePosition.Column] = true;
            }

            // east
            possiblePosition.UpdateValues(Position.Line, Position.Column + 1);
            if (Board.IsValidPosition(possiblePosition) && CanMove(possiblePosition))
            {
                matrix[possiblePosition.Line, possiblePosition.Column] = true;
            }

            // southeast
            possiblePosition.UpdateValues(Position.Line + 1, Position.Column + 1);
            if (Board.IsValidPosition(possiblePosition) && CanMove(possiblePosition))
            {
                matrix[possiblePosition.Line, possiblePosition.Column] = true;
            }

            // south
            possiblePosition.UpdateValues(Position.Line + 1, Position.Column);
            if (Board.IsValidPosition(possiblePosition) && CanMove(possiblePosition))
            {
                matrix[possiblePosition.Line, possiblePosition.Column] = true;
            }

            // southwest
            possiblePosition.UpdateValues(Position.Line + 1, Position.Column - 1);
            if (Board.IsValidPosition(possiblePosition) && CanMove(possiblePosition))
            {
                matrix[possiblePosition.Line, possiblePosition.Column] = true;
            }

            // west
            possiblePosition.UpdateValues(Position.Line, Position.Column - 1);
            if (Board.IsValidPosition(possiblePosition) && CanMove(possiblePosition))
            {
                matrix[possiblePosition.Line, possiblePosition.Column] = true;
            }

            // northwest
            possiblePosition.UpdateValues(Position.Line - 1, Position.Column - 1);
            if (Board.IsValidPosition(possiblePosition) && CanMove(possiblePosition))
            {
                matrix[possiblePosition.Line, possiblePosition.Column] = true;
            }

            return matrix;
        }
    }
}