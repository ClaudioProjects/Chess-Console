using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void putPice(ChessPiece cp, Position pos)
        {
            chessPieces[pos.line, pos.column] = cp;
            cp.position = pos;
        }
    }
}
