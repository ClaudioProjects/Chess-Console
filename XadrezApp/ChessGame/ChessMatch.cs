using XadrezApp.Board;
using XadrezApp.Board.Exceptions;

namespace XadrezApp.ChessGame
{
    internal class ChessMatch
    {
        public BoardTab board { get; private set; }
        public int turn { get; private set; }
        public Color actualPlayer { get; private set; }
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

        public void makesMove(Position origin, Position destiny)
        {
            performMovement(origin, destiny);
            turn++;
            altPlayer();
        }

        private void altPlayer()
        {
            actualPlayer = actualPlayer == Color.White ? Color.Black : Color.White;
        }

        private void putPieces()
        {
            board.putPice(new King(board, Color.White), new PositionChess('c', 1).toPosition());
            board.putPice(new King(board, Color.White), new PositionChess('c', 2).toPosition());
            board.putPice(new Tower(board, Color.White), new PositionChess('c', 3).toPosition());
        }

        public void validOriginPosition(Position pos)
        {
            if (board.chessPiece(pos) == null) throw new BoardException("There is no part in the home position");
            if (actualPlayer != board.chessPiece(pos).color) throw new BoardException("The chosen piece belongs to another player");
            if (!board.chessPiece(pos).existPossibleMoves()) throw new BoardException("There are no possible moves for this piece");
        }

        public void validDestinyPosition(Position origin, Position destiny)
        {
            if (!board.chessPiece(origin).canMoveToTargetPosition(destiny)) throw new BoardException("Destiny position invalid");
        }
    }
}
