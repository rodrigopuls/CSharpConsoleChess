using Chessboard;

namespace Game
{
    public class BoardPosition
    {
        public char Column { get; set; }
        public int Line { get; set; }

        public BoardPosition(char column, int line)
        {
            Column = column;
            Line = line;
        }

        public Position ToPosition(){
            return new Position(8 - Line, Column - 'a');
        }

        public override string ToString()
        {
            return $"{Column}{Line}";
        }
    }
}