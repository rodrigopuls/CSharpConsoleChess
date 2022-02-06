using Chessboard;

namespace Game
{
    public class Pawn : Piece
    {
        private Match _match { get; set; }

        public Pawn(Board board, Color color, Match match) : base(board, color)
        {
            _match = match;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool HasEnemy(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece != null && piece?.Color != Color;
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
                Position possiblePosition2 = new Position(Position.Line - 1, Position.Column);
                if (
                    Board.IsValidPosition(possiblePosition2) &&
                    FreePosition(possiblePosition2) &&
                    Board.IsValidPosition(possiblePosition) &&
                    FreePosition(possiblePosition) &&
                    MovementQuantity == 0)
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

                // special move en passant
                if (Position.Line == 3)
                {
                    Position leftPosition = new Position(Position.Line, Position.Column - 1);
                    if (Board.IsValidPosition(leftPosition) && HasEnemy(leftPosition) && Board.Piece(leftPosition) == _match.VunerableToEnPassantMove)
                    {
                        matrix[leftPosition.Line - 1, leftPosition.Column] = true;
                    }

                    Position rightPosition = new Position(Position.Line, Position.Column + 1);
                    if (Board.IsValidPosition(rightPosition) && HasEnemy(rightPosition) && Board.Piece(rightPosition) == _match.VunerableToEnPassantMove)
                    {
                        matrix[rightPosition.Line - 1, rightPosition.Column] = true;
                    }
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

                // special move en passant
                if (Position.Line == 4)
                {
                    Position leftPosition = new Position(Position.Line, Position.Column - 1);
                    if (Board.IsValidPosition(leftPosition) && HasEnemy(leftPosition) && Board.Piece(leftPosition) == _match.VunerableToEnPassantMove)
                    {
                        matrix[leftPosition.Line + 1, leftPosition.Column] = true;
                    }

                    Position rightPosition = new Position(Position.Line, Position.Column + 1);
                    if (Board.IsValidPosition(rightPosition) && HasEnemy(rightPosition) && Board.Piece(rightPosition) == _match.VunerableToEnPassantMove)
                    {
                        matrix[rightPosition.Line + 1, rightPosition.Column] = true;
                    }
                }
            }

            return matrix;
        }

    }
}