using Chessboard;

namespace Game
{
    public class Pawn : Piece
    {
        public Pawn(Board board, Color color) : base(board, color)
        {

        }

        public override string ToString()
        {
            return "P";
        }

        private bool HasEnemy(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece == null && piece?.Color != Color;
        }

        private bool FreePosition(Position position)
        {
            return Board.Piece(position) == null;
        }

        public override bool[,] GetAllPossibleMoves()
        {
            bool[,] matrix = new bool[Board.Lines, Board.Columns];

            Position possiblePosition = new Position(0, 0);

            if (Color == Color.White)
            {
                possiblePosition.UpdateValues(Position.Line - 1, Position.Column);
                if (Board.IsValidPosition(possiblePosition) && FreePosition(possiblePosition))
                {
                    matrix[possiblePosition.Line, possiblePosition.Column] = true;
                }

                possiblePosition.UpdateValues(Position.Line - 2, Position.Column);
                if (Board.IsValidPosition(possiblePosition) && FreePosition(possiblePosition) && MovementQuantity == 0)
                {
                    matrix[possiblePosition.Line, possiblePosition.Column] = true;
                }

                possiblePosition.UpdateValues(Position.Line - 1, Position.Column - 1);
                if (Board.IsValidPosition(possiblePosition) && HasEnemy(possiblePosition))
                {
                    matrix[possiblePosition.Line, possiblePosition.Column] = true;
                }

                possiblePosition.UpdateValues(Position.Line - 1, Position.Column + 1);
                if (Board.IsValidPosition(possiblePosition) && HasEnemy(possiblePosition))
                {
                    matrix[possiblePosition.Line, possiblePosition.Column] = true;
                }
            }
            else
            {
                possiblePosition.UpdateValues(Position.Line + 1, Position.Column);
                if (Board.IsValidPosition(possiblePosition) && FreePosition(possiblePosition))
                {
                    matrix[possiblePosition.Line, possiblePosition.Column] = true;
                }

                possiblePosition.UpdateValues(Position.Line + 2, Position.Column);
                if (Board.IsValidPosition(possiblePosition) && FreePosition(possiblePosition) && MovementQuantity == 0)
                {
                    matrix[possiblePosition.Line, possiblePosition.Column] = true;
                }

                possiblePosition.UpdateValues(Position.Line + 1, Position.Column - 1);
                if (Board.IsValidPosition(possiblePosition) && HasEnemy(possiblePosition))
                {
                    matrix[possiblePosition.Line, possiblePosition.Column] = true;
                }

                possiblePosition.UpdateValues(Position.Line + 1, Position.Column + 1);
                if (Board.IsValidPosition(possiblePosition) && HasEnemy(possiblePosition))
                {
                    matrix[possiblePosition.Line, possiblePosition.Column] = true;
                }
            }

            return matrix;
        }

    }
}