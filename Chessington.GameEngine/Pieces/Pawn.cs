using System.Collections.Generic;
using System.Linq;

namespace Chessington.GameEngine.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Player player) 
            : base(player) { }

        public override IEnumerable<Square> GetAvailableMoves(Board board)
        {
            var currentSquare = board.FindPiece(this);

            var avaliableMoves = new List<Square>();

            if (Player == Player.White)
            {
                avaliableMoves.Add(new Square(currentSquare.Row - 1, currentSquare.Col));
            }
            else
            {
                avaliableMoves.Add(new Square(currentSquare.Row + 1, currentSquare.Col));
            }

            return avaliableMoves;

        }
    }
}