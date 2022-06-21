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

        private bool canMove(Position pos)
        {
            ChessPiece cp = board.chessPiece(pos);
            return cp == null || cp.color != this.color; 
        }

        public override bool[,] possibleMoves()
        {
            bool[,] mat = new bool[board.lines, board.columns];

            Position pos = new(0, 0);

            // UP

            pos.setPosition(position.line - 1, position.column);
            if (board.isValidPosition(pos) && canMove(pos)) mat[pos.line, pos.column] = true;

            // UP Right

            pos.setPosition(position.line - 1, position.column + 1);
            if (board.isValidPosition(pos) && canMove(pos)) mat[pos.line, pos.column] = true;

            // UP Left

            pos.setPosition(position.line - 1, position.column - 1);
            if (board.isValidPosition(pos) && canMove(pos)) mat[pos.line, pos.column] = true;

            // Right

            pos.setPosition(position.line, position.column + 1);
            if (board.isValidPosition(pos) && canMove(pos)) mat[pos.line, pos.column] = true;

            // Left

            pos.setPosition(position.line, position.column - 1);
            if (board.isValidPosition(pos) && canMove(pos)) mat[pos.line, pos.column] = true;

            // Down

            pos.setPosition(position.line + 1, position.column );
            if (board.isValidPosition(pos) && canMove(pos)) mat[pos.line, pos.column] = true;

            // Down Right

            pos.setPosition(position.line + 1, position.column + 1);
            if (board.isValidPosition(pos) && canMove(pos)) mat[pos.line, pos.column] = true;

            // Down Left

            pos.setPosition(position.line + 1, position.column - 1);
            if (board.isValidPosition(pos) && canMove(pos)) mat[pos.line, pos.column] = true;

            return mat;
        }
    }
}
