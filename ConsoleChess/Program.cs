using System;
using Chessboard;
using ConsoleChess.Chessboard;
using Game;

namespace ConsoleChess
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Match match = new Match();

                while (!match.GameOver)
                {

                    try
                    {
                        Console.Clear();
                        Canvas.PrintMatch(match);

                        Console.WriteLine();
                        Console.Write("Type initial position: ");
                        Position initial = Canvas.ReadPieceMovimentInput().ToPosition();
                        match.ValidateInitialPosition(initial);

                        bool[,] GetPossibleMoves = match.Board.Piece(initial).GetAllPossibleMoves();

                        Console.Clear();
                        Canvas.PrintBoard(match.Board, GetPossibleMoves);

                        Console.WriteLine();
                        Console.Write("Type final position: ");
                        Position final = Canvas.ReadPieceMovimentInput().ToPosition();
                        match.ValidateFinalPosition(initial, final);

                        match.PlayTurn(initial, final);

                    }
                    catch (BoardException ex)
                    {
                        Console.WriteLine("\r\n" + ex.Message);
                        Console.ReadKey();
                    }
                }
                Console.Clear();
                Canvas.PrintBoard(match.Board);
            }
            catch (BoardException ex)
            {
                Console.WriteLine(ex.Message);
            }

            // ChessboardPosition c = new ChessboardPosition('a', 1);
            // Console.WriteLine(c.ToString());
            // Console.WriteLine(c.ToPosition());


            Console.ReadKey();
        }
    }
}
