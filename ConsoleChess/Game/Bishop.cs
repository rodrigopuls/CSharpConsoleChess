using Chessboard;

namespace Game
{
    public class Bishop : Piece
    {
        public Bishop(Board board, Color color) : base(board, color)
        {

        }

        public override string ToString()
        {
            return "B";
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

            // northwest
            possiblePosition.UpdateValues(Position.Line - 1, Position.Column - 1);
            while (Board.IsValidPosition(possiblePosition) && CanMove(possiblePosition))
            {
                matrix[possiblePosition.Line, possiblePosition.Column] = true;

                if (Board.Piece(possiblePosition) != null && Board.Piece(possiblePosition).Color != Color)
                {
                    break;
                }
                possiblePosition.UpdateValues(possiblePosition.Line - 1, possiblePosition.Column - 1);
            }

            // northeast
            possiblePosition.UpdateValues(Position.Line - 1, Position.Column + 1);
            while (Board.IsValidPosition(possiblePosition) && CanMove(possiblePosition))
            {
                matrix[possiblePosition.Line, possiblePosition.Column] = true;

                if (Board.Piece(possiblePosition) != null && Board.Piece(possiblePosition).Color != Color)
                {
                    break;
                }
                possiblePosition.UpdateValues(possiblePosition.Line - 1, possiblePosition.Column + 1);
            }

            // southeast
            possiblePosition.UpdateValues(Position.Line + 1, Position.Column + 1);
            while (Board.IsValidPosition(possiblePosition) && CanMove(possiblePosition))
            {
                matrix[possiblePosition.Line, possiblePosition.Column] = true;

                if (Board.Piece(possiblePosition) != null && Board.Piece(possiblePosition).Color != Color)
                {
                    break;
                }
                possiblePosition.UpdateValues(possiblePosition.Line + 1, possiblePosition.Column + 1);
            }

            // southwest
            possiblePosition.UpdateValues(Position.Line + 1, Position.Column - 1);
            while (Board.IsValidPosition(possiblePosition) && CanMove(possiblePosition))
            {
                matrix[possiblePosition.Line, possiblePosition.Column] = true;

                if (Board.Piece(possiblePosition) != null && Board.Piece(possiblePosition).Color != Color)
                {
                    break;
                }
                possiblePosition.UpdateValues(possiblePosition.Line + 1, possiblePosition.Column - 1);
            }
            return matrix;
        }

    }
}