using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Queen : Piece
    {
        public Queen(Player player)
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