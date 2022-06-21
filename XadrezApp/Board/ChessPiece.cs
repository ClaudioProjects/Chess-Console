namespace XadrezApp.Board
{
    internal abstract class ChessPiece
    {
        public Position position { get; set; }
        public Color color { get; set; }
        public int numberOfMoves { get; protected set; }
        public BoardTab board { get; protected set; }

        public ChessPiece(Color color, BoardTab board)
        {
            this.position = null;
            this.color = color;
            this.board = board;
            this.numberOfMoves = 0;
        }

        public void addMovement()
        {
            this.numberOfMoves++;
        }

        public bool existPossibleMoves()
        {
            bool[,] mat = possibleMoves();

            for (int i = 0; i <board.lines; i++)
            {
                for (int j = 0; j < board.columns; j++)
                {
                    if (mat[i, j]) return true;
                }
            }
            return false;
        }

        public bool canMoveToTargetPosition(Position pos)
        {
            return possibleMoves()[pos.line, pos.column];
        }

        public abstract bool[,] possibleMoves();
    }
}
