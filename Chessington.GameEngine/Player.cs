using System;

namespace Chessington.GameEngine
{
    public enum Player
    {
        White,
        Black
    }

    public static class PlayerMethods
    {
        public static Player opposingPlayer(this Player player)
        {
            switch (player)
            {
                case Player.White:
                    return Player.Black;
                case Player.Black:
                    return Player.White;
                default:
                    throw new Exception("Not a player");
            }
        }
    }
}