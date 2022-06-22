using XadrezApp.Board;

namespace XadrezApp.ChessGame
{
    internal class Horse : ChessPiece
    {
        public Horse(BoardTab board, Color color) : base(color, board) { }

        public override string ToString()
        {
            return "H";
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

            // Up right
            pos.setPosition(position.line - 2, position.column + 1);
            if (board.isValidPosition(pos) && canMove(pos)) mat[pos.line, pos.column] = true;

            // Up left 

            pos.setPosition(position.line - 2, position.column - 1);
            if (board.isValidPosition(pos) && canMove(pos)) mat[pos.line, pos.column] = true;

            // Down Right

            pos.setPosition(position.line + 2, position.column + 1);
            if (board.isValidPosition(pos) && canMove(pos)) mat[pos.line, pos.column] = true;

            // Down left

            pos.setPosition(position.line + 2, position.column - 1);
            if (board.isValidPosition(pos) && canMove(pos)) mat[pos.line, pos.column] = true;

            // Right up

            pos.setPosition(position.line - 1, position.column + 2);
            if (board.isValidPosition(pos) && canMove(pos)) mat[pos.line, pos.column] = true;

            // Right down

            pos.setPosition(position.line + 1, position.column + 2);
            if (board.isValidPosition(pos) && canMove(pos)) mat[pos.line, pos.column] = true;

            // Left up

            pos.setPosition(position.line - 1, position.column - 2);
            if (board.isValidPosition(pos) && canMove(pos)) mat[pos.line, pos.column] = true;

            // Left Down

            pos.setPosition(position.line + 1, position.column - 2);
            if (board.isValidPosition(pos) && canMove(pos)) mat[pos.line, pos.column] = true;

            return mat;
        }
    }
}
