using System.Collections.Generic;
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

            for (int i = 0; i < GameSettings.BoardSize; i++)
            {
                if (i != currentSquare.Col)
                {
                    availableMoves.Add(new Square(currentSquare.Row, i));
                }

                if (i != currentSquare.Row)
                {
                    availableMoves.Add(new Square(i, currentSquare.Col));
                }
            }

            return availableMoves;
        }
    }
}