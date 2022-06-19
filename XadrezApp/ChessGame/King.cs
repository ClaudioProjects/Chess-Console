using XadrezApp.Board;

namespace XadrezApp.ChessGame
{
    internal class King : ChessPiece
    {
        public King(BoardTab board, Color color) : base(color, board) {}

        public override string ToString()
        {
            return "K";
        }
    }
}
