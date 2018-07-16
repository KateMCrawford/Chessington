using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(Player player)
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var currentSquare = board.FindPiece(this);
            var availableMoves = new List<Square>();
            var differenceCol = 0;
            var pos = 0;

            for (int i = 0; i < GameSettings.BoardSize; i++)
            {
                differenceCol = currentSquare.Col - i;
                pos = currentSquare.Row - differenceCol;
                if (i != currentSquare.Col && pos < GameSettings.BoardSize)
                {
                    availableMoves.Add(new Square(pos, i));
                }

                pos = currentSquare.Row + differenceCol;
                if (i != currentSquare.Col && pos < GameSettings.BoardSize)
                {
                    availableMoves.Add(new Square(pos, i));
                }

            }

            return availableMoves;
        }
    }
}