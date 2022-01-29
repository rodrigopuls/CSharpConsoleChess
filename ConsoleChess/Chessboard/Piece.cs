namespace Chessboard
{
    public abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MovementQuantity { get; protected set; }
        public Board Board { get; protected set; }

        public Piece(Board board, Color color)
        {
            Position = null;
            Board = board;
            Color = color;
            MovementQuantity = 0;
        }

        public void IncrementMovementQuantity()
        {
            MovementQuantity++;
        }

        public void DecrementMovementQuantity()
        {
            MovementQuantity--;
        }

        public bool HasPossibleMoves()
        {
            bool[,] possibleMoves = GetAllPossibleMoves();
            for (int l = 0; l < Board.Lines; l++)
            {
                for (int c = 0; c < Board.Columns; c++)
                {
                    if (possibleMoves[l, c])
                        return true;
                }
            }

            return false;
        }

        public bool IsPossibleMoveTo(Position position)
        {
            return GetAllPossibleMoves()[position.Line, position.Column];
        }

        public abstract bool[,] GetAllPossibleMoves();

        public string ToUnicodeString()
        {
            switch (this.ToString())
            {
                case "K" when Color == Color.White:
                    return "\u2654";
                case "K" when Color == Color.Black:
                    return "\u265a";
                case "Q" when Color == Color.White:
                    return "\u2655";
                case "Q" when Color == Color.Black:
                    return "\u265b";
                case "R" when Color == Color.White:
                    return "\u2656";
                case "R" when Color == Color.Black:
                    return "\u265c";
                case "B" when Color == Color.White:
                    return "\u2657";
                case "B" when Color == Color.Black:
                    return "\u2657";
                case "H" when Color == Color.White:
                    return "\u2658";
                case "H" when Color == Color.Black:
                    return "\u265e";
                case "P" when Color == Color.White:
                    return "\u2659";
                case "P" when Color == Color.Black:
                    return "\u265f";
                default:
                    return "";
            }
        }
    }
}