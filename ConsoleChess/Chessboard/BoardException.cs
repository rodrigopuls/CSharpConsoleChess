using System;

namespace ConsoleChess.Chessboard
{
    public class BoardException :  ApplicationException
    {
        public BoardException(string message) : base(message)
        {

        }
    }
}