namespace XadrezApp.Board
{
    internal class ChessPiece
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
    }
}
