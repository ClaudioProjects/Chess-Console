using XadrezApp.Board.Exceptions;

namespace XadrezApp.Board
{
    internal class BoardTab
    {
        public int lines { get; set; }
        public int columns { get; set; }
        private ChessPiece[,] chessPieces;

        public BoardTab(int lines, int columns)
        {
            this.lines = lines;
            this.columns = columns;
            chessPieces = new ChessPiece[lines, columns];
        }

        public ChessPiece chessPiece(int line, int column)
        {
            return chessPieces[line, column];
        }
        public ChessPiece chessPiece(Position pos)
        {
            return chessPieces[pos.line, pos.column]; 
        }

        public bool existChessPiece(Position pos)
        {
            validPosition(pos);
            return chessPiece(pos) != null;
        }

        public void putPice(ChessPiece cp, Position pos)
        {
            if (existChessPiece(pos)) throw new BoardException("There is already a piece in that position");
            chessPieces[pos.line, pos.column] = cp;
            cp.position = pos;
        }

        public ChessPiece removePiece(Position pos)
        {
            if (chessPiece(pos) == null) return null;
            ChessPiece aux = chessPiece(pos);
            aux.position = null;
            chessPieces[pos.line, pos.column] = null;
            return aux;
        }

        public bool isValidPosition(Position pos)
        {
            if(pos.line < 0 || pos.line >= lines || pos.column < 0 || pos.column >= columns) return false;
            return true;
        }

        public void validPosition(Position pos)
        {
            if (!isValidPosition(pos)) throw new BoardException("Position inavalid");
        }
    }
}
