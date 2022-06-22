using XadrezApp.Board;

namespace XadrezApp.ChessGame
{
    internal class King : ChessPiece
    {
        private ChessMatch chessMatch;

        public King(BoardTab board, Color color, ChessMatch chessMatch) : base(color, board)
        {
            this.chessMatch = chessMatch;
        }

        public override string ToString()
        {
            return "K";
        }

        private bool canMove(Position pos)
        {
            ChessPiece cp = board.chessPiece(pos);
            return cp == null || cp.color != this.color;
        }

        private bool existPieceToSpecialPlay(Position pos)
        {
            ChessPiece cp = board.chessPiece(pos);
            return cp != null && cp is Tower && cp.color == color && cp.numberOfMoves == 0;
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

            pos.setPosition(position.line + 1, position.column);
            if (board.isValidPosition(pos) && canMove(pos)) mat[pos.line, pos.column] = true;

            // Down Right

            pos.setPosition(position.line + 1, position.column + 1);
            if (board.isValidPosition(pos) && canMove(pos)) mat[pos.line, pos.column] = true;

            // Down Left

            pos.setPosition(position.line + 1, position.column - 1);
            if (board.isValidPosition(pos) && canMove(pos)) mat[pos.line, pos.column] = true;

            // Special Play

            if (numberOfMoves == 0 && !chessMatch.check)
            {
                // Castling small

                Position posTower = new Position(position.line, position.column + 3);
                if (existPieceToSpecialPlay(posTower))
                {
                    Position p1 = new Position(position.line, position.column + 1);
                    Position p2 = new Position(position.line, position.column + 2);

                    if(board.chessPiece(p1) == null && board.chessPiece(p2) == null) mat[position.line, position.column + 2] = true;
                }

                // Castling larger

                Position posTowerL = new Position(position.line, position.column - 4);
                if (existPieceToSpecialPlay(posTowerL))
                {
                    Position p1 = new Position(position.line, position.column - 1);
                    Position p2 = new Position(position.line, position.column - 2);
                    Position p3 = new Position(position.line, position.column - 3);

                    if (board.chessPiece(p1) == null && board.chessPiece(p2) == null && board.chessPiece(p3) == null)
                        mat[position.line, position.column - 2] = true;
                }
            }

            return mat;
        }
    }
}
