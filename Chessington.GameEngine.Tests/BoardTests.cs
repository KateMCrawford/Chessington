using Chessington.GameEngine.Pieces;
using FluentAssertions;
using NUnit.Framework;

namespace Chessington.GameEngine.Tests
{
    [TestFixture]
    public class BoardTests
    {
        [Test]
        public void PawnCanBeAddedToBoard()
        {
            var board = new Board();
            var pawn = new Pawn(Player.White);
            board.AddPiece(Square.At(0, 0), pawn);

            board.GetPiece(Square.At(0, 0)).Should().BeSameAs(pawn);
        }

        [Test]
        public void PawnCanBeFoundOnBoard()
        {
            var board = new Board();
            var pawn = new Pawn(Player.White);
            var square = Square.At(6, 4);
            board.AddPiece(square, pawn);

            var location = board.FindPiece(pawn);

            location.Should().Be(square);
        }

        [Test]
        public void CanBlockCheck()
        {
            var board = new Board();

            var king = new King(Player.Black);
            var bishop = new Bishop(Player.White);
            var queen = new Queen(Player.Black);

            board.AddPiece(Square.At(0, 2), king);
            board.AddPiece(Square.At(2, 0), bishop);
            board.AddPiece(Square.At(0, 1), queen);

            queen.GetAvailableMoves(board).Should().Contain(Square.At(1, 1));

        }

        [Test]
        public void CheckmateIsRecognised()
        {
            var board = new Board();

            var king = new King(Player.Black);
            var queen1 = new Queen(Player.White);
            var queen2 = new Queen(Player.White);

            board.AddPiece(Square.At(0, 0), king);
            board.AddPiece(Square.At(3, 0), queen1);
            board.AddPiece(Square.At(3, 1), queen2);

            board.PlayerHasWon(board.CurrentPlayer).Should().Be(true);
        }

        [Test]
        public void StalemateIsRecognised()
        {
            var board = new Board(Player.Black);

            var whiteKing = new King(Player.White);
            var whiteQueen = new Queen(Player.White);
            var blackKing = new King(Player.Black);

            board.AddPiece(Square.At(0, 7), blackKing);
            board.AddPiece(Square.At(1, 5), whiteKing);
            board.AddPiece(Square.At(2, 6), whiteQueen);

            board.PlayerHasNoMoves(Player.Black).Should().Be(true);
            board.PlayerIsInCheck(Player.Black).Should().Be(false);
        }
    }
}
