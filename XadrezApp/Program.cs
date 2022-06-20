using XadrezApp.Board;
using XadrezApp.ChessGame;
using XadrezApp.Board.Exceptions;

namespace XadrezApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessMatch chessMatch = new();

                while (!chessMatch.finished)
                {
                    Console.Clear();
                    Screen.ShowBoard(chessMatch.board);
                    Console.WriteLine("");
                    Console.Write("Origin: ");
                    Position origin = Screen.readPositionChess().toPosition();
                    Console.Write("Destiny: ");
                    Position destiny = Screen.readPositionChess().toPosition();

                    chessMatch.performMovement(origin, destiny);
                }

            }
            catch (BoardException e) { Console.WriteLine(e.Message); }
        }
    }
}