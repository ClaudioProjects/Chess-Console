using XadrezApp.Board;

namespace XadrezApp
{
    internal class Screen
    {
        public static void ShowBoard(BoardTab board)
        {
            for (int i = 0; i < board.lines; i++)
            {
                for (int j = 0; j < board.columns; j++)
                {
                    if (board.chessPiece(i, j) != null)
                    {
                        Console.Write(board.chessPiece(i, j) + " ");
                    }
                    else 
                    {
                        Console.Write("- "); 
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
