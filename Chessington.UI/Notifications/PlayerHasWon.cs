using Chessington.GameEngine;

namespace Chessington.UI.Notifications
{
    public class PlayerHasWon
    {
        public PlayerHasWon(Player player)
        {
            Player = player;
        }

        public Player Player { get; private set; }
    }
}