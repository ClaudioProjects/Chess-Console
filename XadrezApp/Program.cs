using XadrezApp.Board;
using XadrezApp.ChessGame;

namespace XadrezApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BoardTab board = new BoardTab(8, 8);

            board.putPice(new King(board, Color.Black), new Position(0, 0));
            board.putPice(new Tower(board, Color.Black), new Position(0, 1));
            board.putPice(new Tower(board, Color.Black), new Position(0, 2));

            Screen.ShowBoard(board);
        }
    }
}