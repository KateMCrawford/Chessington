using Chessington.GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chessington.GameEngine.Pieces;

namespace Chessington.GameEngine
{
    static class CastlingHelper
    {
        private static bool CanCastle(Board board, Rook rook, King king)
        {
            var rookPos = board.FindPiece(rook);
            var kingPos = board.FindPiece(king);
            if (!rook.hasMoved && !king.hasMoved)
            {
                var min = rookPos.Col < 4 ? rookPos.Col + 1 : 5;
                var max = rookPos.Col > 4 ? rookPos.Col : 4;
                var allEmptyAndSafe = true;
                for (int i = min; i < max; i++)
                {
                    var square = Square.At(rookPos.Row, i);
                    if (board.GetPiece(square) != null ||
                        board.MovePutsPlayerInCheck(king.Player, kingPos, square))
                    {
                        allEmptyAndSafe = false;
                    }
                }

                return allEmptyAndSafe;
            }
            return false;
        }

        private static bool RookCanCastle(Board board, Rook rook)
        {
            var rookPos = board.FindPiece(rook);
            var king = board.GetPiece(Square.At(rookPos.Row, 4));

            if (king is King)
            {
                return CanCastle(board, rook, (King) king);
            }

            return false;
        }

        public static List<Square> GetRookCastleMoves(Board board, Rook rook)
        {
            var rookPos = board.FindPiece(rook);
            var moves = new List<Square>();
            if (RookCanCastle(board, rook))
            {
                switch (rookPos.Col)
                {
                    case 0:
                        moves.Add(Square.At(rookPos.Row, 3));
                        break;
                    case 7:
                        moves.Add(Square.At(rookPos.Row, 5));
                        break;
                }
            }

            return moves;
        }

        public static List<Square> GetKingCastleMoves(Board board, King king)
        {
            var moves = new List<Square>();
            var kingPos = board.FindPiece(king);

            if (!board.PlayerIsInCheck(king.Player))
            {
                var rook = board.GetPiece(Square.At(kingPos.Row, 0));
                if ((rook is Rook) && CanCastle(board, (Rook) rook, king))
                {
                    moves.Add(Square.At(kingPos.Row, 2));
                }

                rook = board.GetPiece(Square.At(kingPos.Row, 7));
                if ((rook is Rook) && CanCastle(board, (Rook) rook, king))
                {
                    moves.Add(Square.At(kingPos.Row, 6));
                }
            }

            return moves;
        }

        public static bool IsKingCastleMove(Square from, Square to)
        {
            return (from.Row == to.Row && Math.Abs(from.Col - to.Col) == 2);
        }

        public static Square[] GetCastlingMoveRook(Board board, Square kingFrom, Square kingTo)
        {
            int rookFrom = 0;
            int rookTo = 0;

            switch (kingFrom.Col - kingTo.Col)
            {
                //left rook
                case 2:
                    rookFrom = 0;
                    rookTo = 3;
                    break;
                // right rook
                case -2:
                    rookFrom = 7;
                    rookTo = 5;
                    break;
            }

            return new Square[] { Square.At(kingFrom.Row, rookFrom), Square.At(kingFrom.Row, rookTo) };
        }
    }
}
