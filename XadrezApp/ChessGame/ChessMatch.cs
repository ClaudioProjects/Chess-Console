using XadrezApp.Board;

namespace XadrezApp.ChessGame
{
    internal class ChessMatch
    {
        public BoardTab board { get; private set; }
        private int turn;
        private Color actualPlayer;
        public bool finished { get; set; }

        public ChessMatch()
        {
            board = new BoardTab(8, 8);
            turn = 1;
            actualPlayer = Color.White;
            finished = false;
            putPieces();
        }

        public void performMovement(Position origin, Position destiny)
        {
            ChessPiece cp = board.removePiece(origin);
            cp.addMovement();
            ChessPiece capturedPiece = board.removePiece(destiny);
            board.putPice(cp, destiny);
        }

        private void putPieces()
        {
            board.putPice(new King(board, Color.Black), new PositionChess('c', 1).toPosition());
            board.putPice(new Tower(board, Color.Black), new PositionChess('c', 2).toPosition());
            board.putPice(new Tower(board, Color.White), new PositionChess('c', 3).toPosition());
        }
    }
}
