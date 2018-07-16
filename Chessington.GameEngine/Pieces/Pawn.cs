using System;
using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Player player) 
            : base(player) { }

        private bool hasMoved = false;

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var currentSquare = board.FindPiece(this);
            var availableMoves = new List<Square>();

            if (Player == Player.White)
            {
                var moves = GetMoves(currentSquare, board, -1);
                availableMoves = availableMoves.Concat(moves).ToList();
            }
            else
            {
                var moves = GetMoves(currentSquare, board, 1);
                availableMoves = availableMoves.Concat(moves).ToList();
            }

            return availableMoves;
        }

        public override void MoveTo(Board board, Square newSquare)
        {
            base.MoveTo(board, newSquare);
            hasMoved = true;
        }

        private IEnumerable<Square> GetMoves(Square currentSquare, Board board, int direction)
        {
            var moves = new List<Square>();

            // one square
            Square move = new Square(currentSquare.Row + 1*direction, currentSquare.Col);
            if (board.IsSquareEmpty(move))
            {
                moves.Add(move);
                
                // two squares
                move = new Square(currentSquare.Row + 2 * direction, currentSquare.Col);
                if (!hasMoved && board.IsSquareEmpty(move))
                {
                    moves.Add(move);
                }
            }

            // diagonal
            move = new Square(currentSquare.Row + 1 * direction, currentSquare.Col - 1);
            if (board.ContainsOpposingPiece(move, Player))
            {
                moves.Add(move);
            }
            move = new Square(currentSquare.Row + 1 * direction, currentSquare.Col + 1);
            if (board.ContainsOpposingPiece(move, Player))
            {
                moves.Add(move);
            }

            return moves;
        }
    }
}