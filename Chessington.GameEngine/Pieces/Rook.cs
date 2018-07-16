﻿using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Rook : Piece
    {
        public Rook(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var currentSquare = board.FindPiece(this);
            var availableMoves = new List<Square>();
            
            availableMoves = availableMoves
                .Concat(GetMoves(currentSquare, board, 0, 1))
                .Concat(GetMoves(currentSquare, board, 0, -1))
                .Concat(GetMoves(currentSquare, board, 1, 0))
                .Concat(GetMoves(currentSquare, board, -1, 0))
                .ToList();

            return availableMoves;
        }

        private IEnumerable<Square> GetMoves(Square currentSquare, Board board, int rowDirection, int colDirection)
        {
            var moves = new List<Square>();

            int i = 1;
            Square nextMove = new Square(currentSquare.Row + i * rowDirection, currentSquare.Col + i * colDirection);
            while (i < GameSettings.BoardSize && board.IsSquareEmpty(nextMove))
            {
                moves.Add(nextMove);
                i++;
                nextMove = new Square(currentSquare.Row + i * rowDirection, currentSquare.Col + i * colDirection);
            }

            return moves;
        }
    }
}