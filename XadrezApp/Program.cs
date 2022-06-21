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
                    try
                    {
                        Console.Clear();
                        Screen.ShowBoard(chessMatch.board);
                        Console.WriteLine("");
                        Console.WriteLine("Turn: " + chessMatch.turn);
                        Console.WriteLine("Waiting for play: " + chessMatch.actualPlayer);

                        Console.WriteLine("");

                        Console.Write("Origin: ");
                        Position origin = Screen.readPositionChess().toPosition();
                        chessMatch.validOriginPosition(origin);

                        bool[,] possiblePositions = chessMatch.board.chessPiece(origin).possibleMoves();

                        Console.Clear();
                        Screen.ShowBoard(chessMatch.board, possiblePositions);

                        Console.WriteLine("");

                        Console.Write("Destiny: ");
                        Position destiny = Screen.readPositionChess().toPosition();
                        chessMatch.validDestinyPosition(origin, destiny);

                        chessMatch.makesMove(origin, destiny);
                    } catch (BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }

            }
            catch (BoardException e) { Console.WriteLine(e.Message); }
        }
    }
}