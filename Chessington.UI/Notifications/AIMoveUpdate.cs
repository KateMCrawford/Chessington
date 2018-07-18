using Chessington.GameEngine;

namespace Chessington.UI.Notifications
{
    public class AIMoveUpdate
    {
        public AIMoveUpdate(Square square)
        {
            Square = square;
        }

        public Square Square { get; set; }
    }
}