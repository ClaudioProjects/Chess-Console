using XadrezApp.Board;
using XadrezApp.ChessGame;

namespace XadrezApp
{
    internal class Screen
    {
        public static void showMatch(ChessMatch chessMatch)
        {
            ShowBoard(chessMatch.board);
            Console.WriteLine("");
            showCapturedPieces(chessMatch);
            Console.WriteLine("");
            Console.WriteLine("Turn: " + chessMatch.turn);

            if (!chessMatch.finished)
            {
                Console.WriteLine("Waiting for play: " + chessMatch.actualPlayer);
                if (chessMatch.check) Console.WriteLine("You are in check!"); Console.WriteLine("");
            } else
            {
                Console.WriteLine("");
                Console.WriteLine("Checkmate!");
                Console.WriteLine("Player winner: " + chessMatch.actualPlayer);
            }
        }

        public static void showCapturedPieces(ChessMatch chessMatch)
        {
            Console.WriteLine("Pieces captureds:");
            Console.WriteLine("");
            Console.Write("White: ");
            showCapturedPiecesColor(chessMatch.chessPiecesHash(Color.White));
            Console.Write("Black: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            showCapturedPiecesColor(chessMatch.chessPiecesHash(Color.Black));
            Console.ForegroundColor = aux;
        }

        public static void showCapturedPiecesColor(HashSet<ChessPiece> list)
        {
            Console.Write("[ ");
            foreach(ChessPiece cp in list)
            {
                Console.Write(cp + " ");
            }
            Console.WriteLine("]");
        }

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
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("    A  B  C  D  E  F  G  H");
            Console.ForegroundColor = aux;
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
                    bool isMoveEnemy = false;
                    if (possiblePositions[i, j])
                    {
                        Console.BackgroundColor = selectBackground;
                        Console.ForegroundColor = ConsoleColor.Green;
                        isMoveEnemy = true;
                    }
                    showPiece(board.chessPiece(i, j), isMoveEnemy);
                    Console.BackgroundColor = defaultBackground;
                    Console.ForegroundColor = defaultTextColor;

                }
                Console.WriteLine();
            }
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("    A  B  C  D  E  F  G  H");
            Console.ForegroundColor = aux;
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
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = isMoveEnemy ? ConsoleColor.Green : ConsoleColor.Yellow;
                    Console.Write(" " + cp);
                    Console.ForegroundColor = aux;
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
