using Chessington.GameEngine.Pieces;
using Chessington.UI.Factories;
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

        [Test]
        public void Castling_IsAvailableRook()
        {
            var board = new Board();

            var whiteKing = new King(Player.White);
            var whiteRook = new Rook(Player.White);
            var whiteRook2 = new Rook(Player.White);

            board.AddPiece(Square.At(7, 4), whiteKing);
            board.AddPiece(Square.At(7, 0), whiteRook);
            board.AddPiece(Square.At(7, 7), whiteRook2);

            whiteRook.GetAvailableMoves(board).Should().Contain(Square.At(7, 3));
            whiteRook2.GetAvailableMoves(board).Should().Contain(Square.At(7, 5));
        }

        [Test]
        public void Castling_IsAvailableKing()
        {
            var board = new Board();

            var whiteKing = new King(Player.White);
            var whiteRook = new Rook(Player.White);
            var whiteRook2 = new Rook(Player.White);

            board.AddPiece(Square.At(7, 4), whiteKing);
            board.AddPiece(Square.At(7, 0), whiteRook);
            board.AddPiece(Square.At(7, 7), whiteRook2);

            whiteKing.GetAvailableMoves(board).Should().Contain(Square.At(7, 2));
            whiteKing.GetAvailableMoves(board).Should().Contain(Square.At(7, 6));
        }

        [Test]
        public void Castling_WorksFromKingLeft()
        {
            var board = new Board();

            var whiteKing = new King(Player.White);
            var whiteRook = new Rook(Player.White);
            var whiteRook2 = new Rook(Player.White);

            board.AddPiece(Square.At(7, 4), whiteKing);
            board.AddPiece(Square.At(7, 0), whiteRook);
            board.AddPiece(Square.At(7, 7), whiteRook2);

            board.MovePiece(Square.At(7, 4), Square.At(7, 2));

            (board.GetPiece(Square.At(7, 3)) is Rook).Should().Be(true);
        }

        [Test]
        public void Castling_WorksFromKingRight()
        {
            var board = new Board();

            var whiteKing = new King(Player.White);
            var whiteRook = new Rook(Player.White);
            var whiteRook2 = new Rook(Player.White);

            board.AddPiece(Square.At(7, 4), whiteKing);
            board.AddPiece(Square.At(7, 0), whiteRook);
            board.AddPiece(Square.At(7, 7), whiteRook2);

            board.MovePiece(Square.At(7, 4), Square.At(7, 6));

            (board.GetPiece(Square.At(7, 5)) is Rook).Should().Be(true);
        }

        [Test]
        public void Castling_ThroughCheck_Disallowed_Left()
        {
            var board = new Board();

            var whiteKing = new King(Player.White);
            var whiteRook = new Rook(Player.White);
            var blackRook = new Rook(Player.Black);

            board.AddPiece(Square.At(7, 4), whiteKing);
            board.AddPiece(Square.At(7, 0), whiteRook);
            board.AddPiece(Square.At(5, 3), blackRook);

            whiteKing.GetAvailableMoves(board).Should().NotContain(Square.At(7, 2));
        }

        [Test]
        public void Castling_ThroughCheck_Disallowed_Right()
        {
            var board = new Board();

            var whiteKing = new King(Player.White);
            var whiteRook = new Rook(Player.White);
            var blackRook = new Rook(Player.Black);

            board.AddPiece(Square.At(7, 4), whiteKing);
            board.AddPiece(Square.At(7, 7), whiteRook);
            board.AddPiece(Square.At(5, 5), blackRook);

            whiteKing.GetAvailableMoves(board).Should().NotContain(Square.At(7, 6));
        }
    }
}
