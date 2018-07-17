using System.Collections.Generic;
using System.Linq;
using static Chessington.GameEngine.CastlingHelper;

namespace Chessington.GameEngine.Pieces
{
    public class King : Piece
    {
        public King(Player player)
            : base(player) { }

        public override List<Square> GetAvailableMovesPreCheck(Board board)
        {
            var currentSquare = board.FindPiece(this);
            var availableMoves = new List<Square>();

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (!((i == 0) && (j == 0)))
                    {
                        availableMoves.Add(new Square(currentSquare.Row + i, currentSquare.Col + j));
                    }
                }
            }

            return availableMoves
                .Concat(GetKingCastleMoves(board, this))
                .ToList()
                .FindAll((square) => board.IsSquareEmpty(square) || board.ContainsOpposingPiece(square, Player));
        }
    }
}