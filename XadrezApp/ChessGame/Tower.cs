using XadrezApp.Board;

namespace XadrezApp.ChessGame
{
    internal class Tower : ChessPiece
    {
        public Tower(BoardTab board, Color color) : base(color, board) {}

        public override string ToString()
        {
            return "T";
        }
    }
}

