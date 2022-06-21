using XadrezApp.Board;
using XadrezApp.ChessGame;

namespace XadrezApp
{
    internal class Screen
    {
        public static void ShowBoard(BoardTab board)
        {
            for (int i = 0; i < board.lines; i++)
            {
                Console.Write(" " + (8 - i) + " ");
                for (int j = 0; j < board.columns; j++)
                {
                    showPiece(board.chessPiece(i, j), false);
                }
                Console.WriteLine();
            }
            Console.WriteLine("    A  B  C  D  E  F  G  H");
        }

        public static void ShowBoard(BoardTab board, bool[,] possiblePositions)
        {
            ConsoleColor defaultBackground = Console.BackgroundColor;
            ConsoleColor selectBackground = ConsoleColor.DarkGray;
            ConsoleColor defaultTextColor = Console.ForegroundColor;

            for (int i = 0; i < board.lines; i++)
            {
                Console.Write(" " + (8 - i) + " ");
                for (int j = 0; j < board.columns; j++)
                {
                    if (possiblePositions[i, j])
                    {
                        Console.BackgroundColor = selectBackground;
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    showPiece(board.chessPiece(i, j), true);
                    Console.BackgroundColor = defaultBackground;
                    Console.ForegroundColor = defaultTextColor;

                }
                Console.WriteLine();
            }
            Console.WriteLine("    A  B  C  D  E  F  G  H");
        }

        public static void showPiece(ChessPiece cp, bool isMoveEnemy)
        {
            if (cp == null) Console.Write(" - ");
            else
            {
                if (cp.color == Color.White)
                {
                    Console.Write(" " + cp);
                }
                else
                {
                    if (!isMoveEnemy)
                    {
                        ConsoleColor aux = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(" " + cp);
                        Console.ForegroundColor = aux;
                    } else Console.Write(" " + cp);
                }
                Console.Write(" ");
            }

        }

        public static PositionChess readPositionChess()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int line = int.Parse(s[1] + "");
            return new PositionChess(column, line);
        }
    }
}
