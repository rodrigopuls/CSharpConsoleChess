﻿using System;
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

                while (!match.Ended)
                {

                    try
                    {
                        Console.Clear();
                        Canvas.PrintMatch(match);

                        Console.WriteLine();
                        Console.Write("Type initial position: ");
                        Position initial = Canvas.ReadPieceMovimentInput().ToPosition();
                        match.ValidateInitialPosition(initial);

                        bool[,] possibleMoves = match.Board.Piece(initial).PossibleMoves();

                        Console.Clear();
                        Canvas.PrintBoard(match.Board, possibleMoves);

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