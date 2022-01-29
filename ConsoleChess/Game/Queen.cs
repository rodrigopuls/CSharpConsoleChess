using Chessboard;

namespace Game
{
    public class Queen : Piece
    {
        public Queen(Board board, Color color) : base(board, color)
        {

        }

        public override string ToString()
        {
            return "Q";
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

            // left
            possiblePosition.UpdateValues(Position.Line, Position.Column - 1);
            while (Board.IsValidPosition(possiblePosition) && CanMove(possiblePosition))
            {
                matrix[possiblePosition.Line, possiblePosition.Column] = true;

                if (Board.Piece(possiblePosition) != null && Board.Piece(possiblePosition).Color != Color)
                {
                    break;
                }
                possiblePosition.UpdateValues(possiblePosition.Line, possiblePosition.Column - 1);
            }

            // right
            possiblePosition.UpdateValues(Position.Line, Position.Column + 1);
            while (Board.IsValidPosition(possiblePosition) && CanMove(possiblePosition))
            {
                matrix[possiblePosition.Line, possiblePosition.Column] = true;

                if (Board.Piece(possiblePosition) != null && Board.Piece(possiblePosition).Color != Color)
                {
                    break;
                }
                possiblePosition.UpdateValues(possiblePosition.Line, possiblePosition.Column + 1);
            }

            // up
            possiblePosition.UpdateValues(Position.Line - 1, Position.Column);
            while (Board.IsValidPosition(possiblePosition) && CanMove(possiblePosition))
            {
                matrix[possiblePosition.Line, possiblePosition.Column] = true;

                if (Board.Piece(possiblePosition) != null && Board.Piece(possiblePosition).Color != Color)
                {
                    break;
                }
                possiblePosition.UpdateValues(possiblePosition.Line - 1, possiblePosition.Column);
            }

            // down
            possiblePosition.UpdateValues(Position.Line + 1, Position.Column);
            while (Board.IsValidPosition(possiblePosition) && CanMove(possiblePosition))
            {
                matrix[possiblePosition.Line, possiblePosition.Column] = true;

                if (Board.Piece(possiblePosition) != null && Board.Piece(possiblePosition).Color != Color)
                {
                    break;
                }
                possiblePosition.UpdateValues(possiblePosition.Line + 1, possiblePosition.Column);
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