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
            
            while (board.isValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.chessPiece(pos) != null && board.chessPiece(pos).color != color) break;
                pos.line -= 1;
            }

            // Down

            pos.setPosition(position.line + 1, position.column);

            while (board.isValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.chessPiece(pos) != null && board.chessPiece(pos).color != color) break;
                pos.line += 1;
            }

            // Right

            pos.setPosition(position.line, position.column + 1);

            while (board.isValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.chessPiece(pos) != null && board.chessPiece(pos).color != color) break;
                pos.column += 1;
            }

            // Down

            pos.setPosition(position.line, position.column - 1);

            while (board.isValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.chessPiece(pos) != null && board.chessPiece(pos).color != color) break;
                pos.column -= 1;
            }

            return mat;
        }
    }
}

