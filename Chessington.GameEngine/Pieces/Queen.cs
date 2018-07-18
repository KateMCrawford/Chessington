using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Queen : Piece
    {
        public Queen(Player player)
            : base(player) { }

        public override List<Square> GetAvailableMovesPreCheck(Board board)
        {
            var currentSquare = board.FindPiece(this);
            var availableMoves = new List<Square>();

            availableMoves = availableMoves
                .Concat(GetMovesInDirection(currentSquare, board, 0, 1))
                .Concat(GetMovesInDirection(currentSquare, board, 0, -1))
                .Concat(GetMovesInDirection(currentSquare, board, 1, 0))
                .Concat(GetMovesInDirection(currentSquare, board, -1, 0))
                .Concat(GetMovesInDirection(currentSquare, board, 1, 1))
                .Concat(GetMovesInDirection(currentSquare, board, 1, -1))
                .Concat(GetMovesInDirection(currentSquare, board, -1, 1))
                .Concat(GetMovesInDirection(currentSquare, board, -1, -1))
                .ToList();

            return availableMoves;
        }
    }
}