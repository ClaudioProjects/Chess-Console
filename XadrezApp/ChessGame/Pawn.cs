using XadrezApp.Board;

namespace XadrezApp.ChessGame
{
    internal class Pawn : ChessPiece
    {
        private ChessMatch chessMatch;
        public Pawn(BoardTab board, Color color, ChessMatch chessMatch) : base(color, board)
        {
            this.chessMatch = chessMatch;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool canMove(Position pos)
        {
            ChessPiece cp = board.chessPiece(pos);
            return cp == null || cp.color != this.color;
        }

        private bool existEnemy(Position pos)
        {
            return board.chessPiece(pos) == null ? false : true;
        }

        private bool isValidPassant(char side, ChessPiece cp)
        {
            if (cp == null) return false;
            if (position.line == 3 || position.line == 4)
            {
                if (cp.position.line == position.line) return side == 'R' ? cp.position.column == position.column + 1 : cp.position.column == position.column - 1;
            }
            return false;
        }

        public override bool[,] possibleMoves()
        {
            bool[,] mat = new bool[board.lines, board.columns];

            Position pos = new(0, 0);

            // White moves
            if (color == Color.White)
            {
                // Up

                pos.setPosition(position.line - 1, position.column);
                if (board.isValidPosition(pos) && !existEnemy(pos))
                {
                    mat[pos.line, pos.column] = true;
                    pos.setPosition(position.line - 2, position.column);
                    if (board.isValidPosition(pos) && !existEnemy(pos) && numberOfMoves == 0) mat[pos.line, pos.column] = true;
                }

                // Up right 

                pos.setPosition(position.line - 1, position.column + 1);
                if (board.isValidPosition(pos) && existEnemy(pos)) mat[pos.line, pos.column] = true;

                
                // Up left

                pos.setPosition(position.line - 1, position.column - 1);
                if (board.isValidPosition(pos) && existEnemy(pos)) mat[pos.line, pos.column] = true;

                //passant

                if (position.line == 3)
                {
                    Position left = new Position(position.line, position.column - 1);
                    if (board.isValidPosition(left) && existEnemy(left) && board.chessPiece(left) == chessMatch.vulnerablePiecePassant) mat[left.line - 1, left.column] = true;
                    Position right = new Position(position.line, position.column + 1);
                    if (board.isValidPosition(right) && existEnemy(right) && board.chessPiece(right) == chessMatch.vulnerablePiecePassant) mat[right.line - 1, right.column] = true;
                }
            }


            // Black moves
            if (color == Color.Black)
            {
                // Down

                pos.setPosition(position.line + 1, position.column);
                if (board.isValidPosition(pos) && !existEnemy(pos))
                {
                    mat[pos.line, pos.column] = true;
                    pos.setPosition(position.line + 2, position.column);
                    if (board.isValidPosition(pos) && !existEnemy(pos) && numberOfMoves == 0) mat[pos.line, pos.column] = true;
                }

                // Down right 

                pos.setPosition(position.line + 1, position.column + 1);
                if (board.isValidPosition(pos) && existEnemy(pos)) mat[pos.line, pos.column] = true;

                // Down left

                pos.setPosition(position.line + 1, position.column - 1);
                if (board.isValidPosition(pos) && existEnemy(pos)) mat[pos.line, pos.column] = true;

                // Passant

                if (position.line == 4)
                {
                    Position left = new Position(position.line, position.column - 1);
                    if (board.isValidPosition(left) && existEnemy(left) && board.chessPiece(left) == chessMatch.vulnerablePiecePassant) mat[left.line + 1, left.column] = true;
                    Position right = new Position(position.line, position.column + 1);
                    if (board.isValidPosition(right) && existEnemy(right) && board.chessPiece(right) == chessMatch.vulnerablePiecePassant) mat[right.line + 1, right.column] = true;
                }
            }

            return mat;
        }
    }
}