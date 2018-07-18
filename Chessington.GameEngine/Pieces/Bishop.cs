using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(Player player)
            : base(player) { }

        public override List<Square> GetAvailableMovesPreCheck(Board board)
        {
            var currentSquare = board.FindPiece(this);
            var availableMoves = new List<Square>();

            availableMoves = availableMoves
                .Concat(GetMovesInDirection(currentSquare, board, 1, 1))
                .Concat(GetMovesInDirection(currentSquare, board, 1, -1))
                .Concat(GetMovesInDirection(currentSquare, board, -1, 1))
                .Concat(GetMovesInDirection(currentSquare, board, -1, -1))
                .ToList();

            return availableMoves;
        }
    }
}