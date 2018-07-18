using System;
using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public abstract class Piece
    {
        protected Piece(Player player)
        {
            Player = player;
            hasMoved = false;
        }

        public bool hasMoved { get; private set; }

        public Player Player { get; private set; }

        public virtual IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var currentSquare = board.FindPiece(this);
            return GetAvailableMovesPreCheck(board)
                .FindAll((move) => !board.MovePutsPlayerInCheck(Player, currentSquare, move));
        }

        public abstract List<Square> GetAvailableMovesPreCheck(Board board);

        public void MoveTo(Board board, Square newSquare)
        {
            var currentSquare = board.FindPiece(this);
            board.MovePiece(currentSquare, newSquare);
            hasMoved = true;
        }

        public IEnumerable<Square> GetMovesInDirection(Square currentSquare, Board board, int rowDirection, int colDirection)
        {
            var moves = new List<Square>();

            int i = 1;
            Square nextMove = new Square(currentSquare.Row + i * rowDirection, currentSquare.Col + i * colDirection);
            while (i < GameSettings.BoardSize && (board.IsSquareEmpty(nextMove)))
            {
                moves.Add(nextMove);
                i++;
                nextMove = new Square(currentSquare.Row + i * rowDirection, currentSquare.Col + i * colDirection);
            }

            if (board.ContainsOpposingPiece(nextMove, Player))
            {
                moves.Add(nextMove);
            }

            return moves;
        }
    }
}