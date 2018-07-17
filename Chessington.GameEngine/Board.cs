using System;
using System.Collections.Generic;
using System.Linq;
using Chessington.GameEngine.Pieces;
using System.Windows;
using static Chessington.GameEngine.CastlingHelper;

namespace Chessington.GameEngine
{
    public class Board
    {
        private readonly Piece[,] board;
        public Player CurrentPlayer { get; private set; }
        public IList<Piece> CapturedPieces { get; private set; }
        public Square? EnPassantSquare = null;
        private Square EnPassantPieceSquare = Square.At(0, 0);

        public Board()
            : this(Player.White) { }

        public Board(Player currentPlayer, Piece[,] boardState = null)
        {
            board = boardState ?? new Piece[GameSettings.BoardSize, GameSettings.BoardSize];
            CurrentPlayer = currentPlayer;
            CapturedPieces = new List<Piece>();
        }

        public void AddPiece(Square square, Piece pawn)
        {
            board[square.Row, square.Col] = pawn;
        }

        public Piece GetPiece(Square square)
        {
            return board[square.Row, square.Col];
        }

        public Square FindPiece(Piece piece)
        {
            for (var row = 0; row < GameSettings.BoardSize; row++)
                for (var col = 0; col < GameSettings.BoardSize; col++)
                    if (board[row, col] == piece)
                        return Square.At(row, col);

            throw new ArgumentException("The supplied piece is not on the board.", "piece");
        }

        public void MovePiece(Square from, Square to)
        {
            var movingPiece = board[from.Row, from.Col];
            if (movingPiece == null) { return; }

            if (movingPiece.Player != CurrentPlayer)
            {
                throw new ArgumentException("The supplied piece does not belong to the current player.");
            }

            //castling moves
            if (movingPiece is King && IsKingCastleMove(from, to))
            {
                var fromTo = GetCastlingMoveRook(this, from, to);
                board[fromTo[1].Row, fromTo[1].Col] = board[fromTo[0].Row, fromTo[0].Col];
                board[fromTo[0].Row, fromTo[0].Col] = null;
            }

            //If the space we're moving to is occupied, we need to mark it as captured.
            if (board[to.Row, to.Col] != null)
            {
                OnPieceCaptured(board[to.Row, to.Col]);
            }

            //en passant capture
            if (movingPiece is Pawn && to == EnPassantSquare)
            {
                OnPieceCaptured(board[EnPassantPieceSquare.Row, EnPassantPieceSquare.Col]);
                board[EnPassantPieceSquare.Row, EnPassantPieceSquare.Col] = null;
            }

            //Move the piece and set the 'from' square to be empty.
            board[to.Row, to.Col] = board[from.Row, from.Col];
            board[from.Row, from.Col] = null;

            // can be en passanted
            if (movingPiece is Pawn && Math.Abs(from.Row - to.Row) == 2)
            {
                EnPassantSquare = Square.At(Math.Abs(from.Row + to.Row) / 2, to.Col);
                EnPassantPieceSquare = to;
            }
            else
            {
                EnPassantSquare = null;
            }

            // pawn promotion
            if (movingPiece is Pawn && (to.Row == 0 || to.Row == GameSettings.BoardSize))
            {
                board[to.Row, to.Col] = new Queen(movingPiece.Player);
            }

            CurrentPlayer = movingPiece.Player == Player.White ? Player.Black : Player.White;
            OnCurrentPlayerChanged(CurrentPlayer);
        }

        public bool MovePutsPlayerInCheck(Player player, Square from, Square to)
        {
            Piece[,] newBoard = (Piece[,])board.Clone();

            Board workingBoard = new Board(player, newBoard);
            workingBoard.MovePiece(from, to);

            return workingBoard.PlayerIsInCheck(player);
        }

        public bool PlayerIsInCheck(Player player)
        {
            for (int i = 0; i < GameSettings.BoardSize; i++)
            {
                for (int j = 0; j < GameSettings.BoardSize; j++)
                {
                    if (ContainsOpposingPiece(Square.At(i, j), player))
                    {
                        foreach (var move in GetPiece(Square.At(i, j)).GetAvailableMovesPreCheck(this))
                        {
                            if (GetPiece(move) is King)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public bool PlayerHasNoMoves(Player player)
        {
            for (int i = 0; i < GameSettings.BoardSize; i++)
            {
                for (int j = 0; j < GameSettings.BoardSize; j++)
                {
                    if (ContainsOpposingPiece(Square.At(i, j), player.opposingPlayer()))
                    {
                        if (GetPiece(Square.At(i, j)).GetAvailableMoves(this).Any())
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public bool PlayerHasWon(Player player)
        {
            return PlayerHasNoMoves(player.opposingPlayer()) && PlayerIsInCheck(player.opposingPlayer());
        }

        public bool IsSquareEmpty(Square testSquare)
        {
            if (testSquare.IsValid())
            {
                return GetPiece(testSquare) == null;
            }
            else
            {
                return false;
            }
        }

        public bool ContainsOpposingPiece(Square square, Player currentPlayer)
        {
            if (IsSquareEmpty(square) || !square.IsValid())
            {
                return false;
            }
            else
            {
                return GetPiece(square).Player != currentPlayer;
            }
        }

        public delegate void PieceCapturedEventHandler(Piece piece);

        public event PieceCapturedEventHandler PieceCaptured;

        protected virtual void OnPieceCaptured(Piece piece)
        {
            var handler = PieceCaptured;
            if (handler != null) handler(piece);
        }

        public delegate void CurrentPlayerChangedEventHandler(Player player);

        public event CurrentPlayerChangedEventHandler CurrentPlayerChanged;

        protected virtual void OnCurrentPlayerChanged(Player player)
        {
            var handler = CurrentPlayerChanged;
            if (handler != null) handler(player);
        }
    }
}
