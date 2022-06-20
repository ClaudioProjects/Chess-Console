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
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.columns; j++)
                {
                    if (board.chessPiece(i, j) != null)
                    {
                        showPiece(board.chessPiece(i, j));
                        Console.Write(" ");
                    }
                    else 
                    {
                        Console.Write("- "); 
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        public static void showPiece(ChessPiece cp)
        {
            if (cp.color == Color.White)
            {
                Console.Write(cp);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(cp);
                Console.ForegroundColor = aux;
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
