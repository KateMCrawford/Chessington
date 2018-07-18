using System;
using System.Collections.Generic;
using Chessington.GameEngine;
using Chessington.GameEngine.Pieces;

namespace Chessington.UI.Factories
{
    /// <summary>
    /// Owns the logic of how to set up a chess board.
    /// </summary>
    public static class StartingPositionFactory
    {
        public static void Setup(Board board)
        {
            for (var i = 0; i < GameSettings.BoardSize; i++)
            {
                board.AddPiece(Square.At(1, i), new Pawn(Player.Black));
                board.AddPiece(Square.At(6, i), new Pawn(Player.White));
            }

            board.AddPiece(Square.At(0, 0), new Rook(Player.Black));
            board.AddPiece(Square.At(0, 1), new Knight(Player.Black));
            board.AddPiece(Square.At(0, 2), new Bishop(Player.Black));
            board.AddPiece(Square.At(0, 3), new Queen(Player.Black));
            board.AddPiece(Square.At(0, 4), new King(Player.Black));
            board.AddPiece(Square.At(0, 5), new Bishop(Player.Black));
            board.AddPiece(Square.At(0, 6), new Knight(Player.Black));
            board.AddPiece(Square.At(0, 7), new Rook(Player.Black));

            board.AddPiece(Square.At(7, 0), new Rook(Player.White));
            board.AddPiece(Square.At(7, 1), new Knight(Player.White));
            board.AddPiece(Square.At(7, 2), new Bishop(Player.White));
            board.AddPiece(Square.At(7, 3), new Queen(Player.White));
            board.AddPiece(Square.At(7, 4), new King(Player.White));
            board.AddPiece(Square.At(7, 5), new Bishop(Player.White));
            board.AddPiece(Square.At(7, 6), new Knight(Player.White));
            board.AddPiece(Square.At(7, 7), new Rook(Player.White));
        }

        private static Random random = new Random();
        public static void RandomSetup(Board board)
        {
            RandomSetupPlayer(board, Player.White);
            RandomSetupPlayer(board, Player.Black);
        }

        private static void RandomSetupPlayer(Board board, Player player)
        {
            board.AddPiece(GetRandomFreePosition(board, player), new King(player));
            for (int i = 0; i < random.Next(31); i++)
            {
                board.AddPiece(GetRandomFreePosition(board, player), GetRandomPiece(player));
            }
        }

        private static Piece GetRandomPiece(Player player)
        {
            var pieceTypes = new Type[] { typeof(Pawn), typeof(Rook), typeof(Knight), typeof(Bishop), typeof(Queen) };
            var randomType = pieceTypes[random.Next(0, pieceTypes.Length)];
            return (Piece) Activator.CreateInstance(randomType, player);
        }

        private static Square GetRandomFreePosition(Board board, Player player)
        {
            int min = player == Player.Black ? 0 : GameSettings.BoardSize / 2;
            int max = player == Player.Black ? GameSettings.BoardSize / 2 : GameSettings.BoardSize;

            int row = random.Next(min, max);
            int col = random.Next(0, GameSettings.BoardSize);

            while (!board.IsSquareEmpty(Square.At(row, col)))
            {
                row = random.Next(min, max);
                col = random.Next(0, GameSettings.BoardSize);
            }

            return Square.At(row, col);
        }
    }
}