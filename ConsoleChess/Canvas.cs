using System;
using System.Collections.Generic;
using Chessboard;
using Game;
namespace ConsoleChess
{
    public class Canvas
    {

        public static void PrintMatch(Match match)
        {
            PrintBoard(match.Board);
            Console.WriteLine();
            Print_capturedPieces(match);
            Console.WriteLine($"Match Turn: {match.Turn}");

            if (!match.GameOver)
            {
                Console.WriteLine($"Waiting Player: {match.CurrentPlayer}\n");
                if (match.Checkmate)
                {
                    Console.WriteLine("CHECK!");
                }
            }
            else
            {
                Console.WriteLine("CHECKMATE!");
                Console.WriteLine($"Winner: {match.CurrentPlayer}");
            }
        }

        public static void PrintBoard(Board board)
        {
            for (int l = 0; l < board.Lines; l++)
            {
                Console.Write($"{8 - l} ");
                for (int c = 0; c < board.Columns; c++)
                {
                    PrintPiece(board.Piece(l, c));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void PrintBoard(Board board, bool[,] GetPossibleMoves)
        {
            ConsoleColor initialalBgColor = Console.BackgroundColor;

            for (int l = 0; l < board.Lines; l++)
            {
                Console.Write($"{8 - l} ");
                for (int c = 0; c < board.Columns; c++)
                {
                    Console.BackgroundColor = GetPossibleMoves[l, c] ? ConsoleColor.DarkGray : initialalBgColor;
                    PrintPiece(board.Piece(l, c));
                    Console.BackgroundColor = initialalBgColor;
                }
                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = initialalBgColor;
        }

        public static BoardPosition ReadPieceMovimentInput()
        {
            string input = Console.ReadLine();
            char column = input[0];
            int line = int.Parse(input[1].ToString());

            return new BoardPosition(column, line);
        }

        public static void PrintPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.Color == Color.White)
                {
                    Console.Write(piece.ToUnicodeString());
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece.ToUnicodeString());
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }

        private static void Print_capturedPieces(Match match)
        {
            Console.WriteLine("### Captured pieces ###");
            Console.Write("Whites: ");
            PrintSet(match.Get_capturedPiecesByColor(Color.White));
            Console.WriteLine("");
            Console.Write("Blacks: ");
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintSet(match.Get_capturedPiecesByColor(Color.Black));
            Console.ForegroundColor = originalColor;
            Console.WriteLine("\n");
        }

        private static void PrintSet(HashSet<Piece> pieces)
        {
            Console.Write($"[{string.Join(',', pieces)}]");
        }
    }
}