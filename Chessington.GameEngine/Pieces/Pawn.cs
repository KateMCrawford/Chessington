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
                availableMoves.Add(new Square(currentSquare.Row - 1, currentSquare.Col));
                if (!hasMoved)
                {
                    availableMoves.Add(new Square(currentSquare.Row - 2, currentSquare.Col));
                }
            }
            else
            {
                availableMoves.Add(new Square(currentSquare.Row + 1, currentSquare.Col));
                if (!hasMoved)
                {
                    availableMoves.Add(new Square(currentSquare.Row + 2, currentSquare.Col));
                }
            }

            return availableMoves;
        }

        public override void MoveTo(Board board, Square newSquare)
        {
            base.MoveTo(board, newSquare);
            hasMoved = true;
        }
    }
}