using Chessington.GameEngine;

namespace Chessington.UI.Notifications
{
    public class CurrentPlayerChanged
    {
        public CurrentPlayerChanged(Player player, Board board)
        {
            Player = player;
            Board = board;
        }

        public Player Player { get; private set; }
        public Board Board { get; private set; }
    }
}