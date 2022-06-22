using XadrezApp.Board;
using XadrezApp.Board.Exceptions;

namespace XadrezApp.ChessGame
{
    internal class ChessMatch
    {
        public BoardTab board { get; private set; }
        public int turn { get; private set; }
        public Color actualPlayer { get; private set; }
        public bool finished { get; set; }
        public bool check { get; private set; }

        private HashSet<ChessPiece> chessPieces;
        private HashSet<ChessPiece> capturedPieces;

        public ChessPiece vulnerablePiecePassant { get; private set; }

        public ChessMatch()
        {
            board = new BoardTab(8, 8);
            turn = 1;
            actualPlayer = Color.White;
            finished = false;
            check = false;
            vulnerablePiecePassant = null;
            capturedPieces = new HashSet<ChessPiece>();
            chessPieces = new HashSet<ChessPiece>();
            putPieces();
        }

        public ChessPiece performMovement(Position origin, Position destiny)
        {
            ChessPiece cp = board.removePiece(origin);
            cp.addMovement();
            ChessPiece capturedPiece = board.removePiece(destiny);
            board.putPice(cp, destiny);

            if (capturedPiece != null) capturedPieces.Add(capturedPiece);

            // Special play 

            if (cp is King && destiny.column == origin.column + 2)
            {
                Position originTower = new Position(origin.line, origin.column + 3);
                Position destinyTower = new Position(origin.line, origin.column + 1);
                ChessPiece tower = board.removePiece(originTower);
                tower.addMovement();
                board.putPice(tower, destinyTower);
            }

            if (cp is King && destiny.column == origin.column - 2)
            {
                Position originTower = new Position(origin.line, origin.column - 4);
                Position destinyTower = new Position(origin.line, origin.column - 1);
                ChessPiece tower = board.removePiece(originTower);
                tower.addMovement();
                board.putPice(tower, destinyTower);
            }

            // Passant

            if (cp is Pawn && (origin.column != destiny.column && capturedPiece == null))
            {
                Position posP = cp.color == Color.White ? new Position(destiny.line + 1, destiny.column) : new Position(destiny.line - 1, destiny.column);
                capturedPiece = board.removePiece(posP);
                capturedPieces.Add(capturedPiece);
            }

            return capturedPiece;
        }

        public void makesMove(Position origin, Position destiny)
        {
            ChessPiece capturedPiece = performMovement(origin, destiny);

            if (isCheck(actualPlayer))
            {
                undoMovement(origin, destiny, capturedPiece);
                throw new BoardException("You can't put yourself in check");
            }

            ChessPiece cp = board.chessPiece(destiny);

            // Special promote

            if (cp is Pawn)
            {
                if((cp.color == Color.White && destiny.line == 0) && (cp.color == Color.Black && destiny.line == 7))
                {
                    cp = board.removePiece(destiny);
                    chessPieces.Remove(cp);
                    ChessPiece queen = new Queen(board, cp.color);
                    board.putPice(queen, destiny);
                }
            }

            if (isCheck(colorEnemy(actualPlayer)))
            {
                check = true;
            }
            else check = false;

            if (isCheckmate(colorEnemy(actualPlayer))) finished = true;
            else
            {
                turn++;
                altPlayer();
            }

            // Passant

            if (cp is Pawn && (destiny.line == origin.line - 2 || destiny.line == origin.line + 2)) vulnerablePiecePassant = cp;
            else vulnerablePiecePassant = null;

        }

        public void undoMovement(Position origin, Position destiny, ChessPiece capturedPiece)
        {
            ChessPiece cp = board.removePiece(destiny);
            cp.decrementMovement();
            if (capturedPiece != null)
            {
                board.putPice(capturedPiece, destiny);
                capturedPieces.Remove(capturedPiece);
            };
            board.putPice(cp, origin);

            // Special play

            if (cp is King && destiny.column == origin.column + 2)
            {
                Position originTower = new Position(origin.line, origin.column + 3);
                Position destinyTower = new Position(origin.line, origin.column + 1);
                ChessPiece K = board.removePiece(destinyTower);
                K.decrementMovement();
                board.putPice(K, originTower);
            }

            if (cp is King && destiny.column == origin.column - 2)
            {
                Position originTower = new Position(origin.line, origin.column - 4);
                Position destinyTower = new Position(origin.line, origin.column - 1);
                ChessPiece K = board.removePiece(destinyTower);
                K.decrementMovement();
                board.putPice(K, originTower);
            }

            // Passant 

            if (cp is Pawn && (origin.column != destiny.column && capturedPiece == vulnerablePiecePassant))
            {
                ChessPiece piece = board.removePiece(destiny);
                Position posP = cp.color == Color.White ? new Position(3, destiny.column) : new Position(4, destiny.column);
                board.putPice(piece, posP);
            }
        }

        private void altPlayer()
        {
            actualPlayer = actualPlayer == Color.White ? Color.Black : Color.White;
        }

        private void putPieces()
        {
            // White
            putNewPiece('a', 1, new Tower(board, Color.White));
            putNewPiece('b', 1, new Horse(board, Color.White));
            putNewPiece('c', 1, new Bishop(board, Color.White));
            putNewPiece('d', 1, new Queen(board, Color.White));
            putNewPiece('e', 1, new King(board, Color.White, this));
            putNewPiece('f', 1, new Bishop(board, Color.White));
            putNewPiece('g', 1, new Horse(board, Color.White));
            putNewPiece('h', 1, new Tower(board, Color.White));
            // putNewPiece('c', 4, new Pawn(board, Color.White, this));
            putPawnInGame(Color.White);

            // Black

            putNewPiece('a', 8, new Tower(board, Color.Black));
            putNewPiece('b', 8, new Horse(board, Color.Black));
            putNewPiece('c', 8, new Bishop(board, Color.Black));
            putNewPiece('d', 8, new Queen(board, Color.Black));
            putNewPiece('e', 8, new King(board, Color.Black, this));
            putNewPiece('f', 8, new Bishop(board, Color.Black));
            putNewPiece('g', 8, new Horse(board, Color.Black));
            putNewPiece('h', 8, new Tower(board, Color.Black));
            putPawnInGame(Color.Black);
        }

        public void putPawnInGame(Color color)
        {
            int sideGame = color == Color.White ? 2 : 7;
            for (int i = 1; i <= 8; i++)
            {
                putNewPiece(char.Parse(char.ConvertFromUtf32(96 + i)), sideGame, new Pawn(board, color, this));
            }
        }

        public void validOriginPosition(Position pos)
        {
            if (board.chessPiece(pos) == null) throw new BoardException("There is no part in the home position");
            if (actualPlayer != board.chessPiece(pos).color) throw new BoardException("The chosen piece belongs to another player");
            if (!board.chessPiece(pos).existPossibleMoves()) throw new BoardException("There are no possible moves for this piece");
        }

        public void validDestinyPosition(Position origin, Position destiny)
        {
            if (!board.chessPiece(origin).canMoveToTargetPosition(destiny)) throw new BoardException("Destiny position invalid");
        }

        public bool isCheck(Color color)
        {
            ChessPiece K = king(color);
            if (K == null) throw new BoardException("Unexpected error");

            foreach (ChessPiece cp in chessPiecesInGame(colorEnemy(color)))
            {
                bool[,] mat = cp.possibleMoves();
                if (mat[K.position.line, K.position.column]) return true;
            }
            return false;
        }

        public bool isCheckmate(Color color)
        {
            if (!isCheck(color)) return false;
            foreach (ChessPiece cp in chessPiecesInGame(color))
            {
                bool[,] mat = cp.possibleMoves();
                for (int i = 0; i < board.lines; i++)
                {
                    for (int j = 0; j < board.columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = cp.position;
                            ChessPiece capturedPiece = performMovement(origin, new Position(i, j));
                            bool check = isCheck(color);
                            undoMovement(origin, new Position(i, j), capturedPiece);
                            if (!check) return false;
                        }
                    }
                }
            }
            return true;
        }

        public void putNewPiece(char column, int line, ChessPiece cp)
        {
            board.putPice(cp, new PositionChess(column, line).toPosition());
            chessPieces.Add(cp);
        }

        public HashSet<ChessPiece> chessPiecesHash(Color color)
        {
            HashSet<ChessPiece> aux = new();
            foreach (ChessPiece cp in capturedPieces) if (cp.color == color) aux.Add(cp);
            return aux;
        }

        public HashSet<ChessPiece> chessPiecesInGame(Color color)
        {
            HashSet<ChessPiece> aux = new();
            foreach (ChessPiece cp in chessPieces) if (cp.color == color) aux.Add(cp);
            aux.ExceptWith(chessPiecesHash(color));
            return aux;
        }

        private Color colorEnemy(Color color)
        {
            if (color == Color.White) return Color.Black;
            return Color.White;
        }

        private ChessPiece king(Color color)
        {
            foreach (ChessPiece cp in chessPiecesInGame(color))
            {
                if (cp is King) return cp;
            }
            return null;
        }
    }
}
