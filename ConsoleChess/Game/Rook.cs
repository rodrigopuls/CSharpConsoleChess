using Chessboard;

namespace Game
{
    public class Rook : Piece
    {
        public Rook(Board board, Color color) : base(board, color)
        {

        }

        public override string ToString()
        {
            return "R";
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
            while (Board.IsValidPosition(possiblePosition) && CanMove(possiblePosition))
            {
                matrix[possiblePosition.Line, possiblePosition.Column] = true;

                if (Board.Piece(possiblePosition) != null && Board.Piece(possiblePosition).Color != Color)
                {
                    break;
                }
                possiblePosition.Line = possiblePosition.Line - 1;
            }

            // south
            possiblePosition.UpdateValues(Position.Line + 1, Position.Column);
            while (Board.IsValidPosition(possiblePosition) && CanMove(possiblePosition))
            {
                matrix[possiblePosition.Line, possiblePosition.Column] = true;

                if (Board.Piece(possiblePosition) != null && Board.Piece(possiblePosition).Color != Color)
                {
                    break;
                }
                possiblePosition.Line = possiblePosition.Line + 1;
            }

            // east
            possiblePosition.UpdateValues(Position.Line, Position.Column + 1);
            while (Board.IsValidPosition(possiblePosition) && CanMove(possiblePosition))
            {
                matrix[possiblePosition.Line, possiblePosition.Column] = true;

                if (Board.Piece(possiblePosition) != null && Board.Piece(possiblePosition).Color != Color)
                {
                    break;
                }
                possiblePosition.Column = possiblePosition.Column + 1;
            }

            // west
            possiblePosition.UpdateValues(Position.Line, Position.Column - 1);
            while (Board.IsValidPosition(possiblePosition) && CanMove(possiblePosition))
            {
                matrix[possiblePosition.Line, possiblePosition.Column] = true;

                if (Board.Piece(possiblePosition) != null && Board.Piece(possiblePosition).Color != Color)
                {
                    break;
                }
                possiblePosition.Column = possiblePosition.Column - 1;
            }
            return matrix;
        }
    }
}