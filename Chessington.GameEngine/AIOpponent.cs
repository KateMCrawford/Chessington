using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chessington.GameEngine
{
    public class AIOpponent
    {
        private static Random rnd = new Random();
        public Player colour = Player.Black;

        private List<Square[]> GetAllMoves(Board board)
        {
            List<Square[]> movesList = new List<Square[]>();
            List<Square[]> aggressiveMovesList = new List<Square[]>();
            for (int i = 0; i < GameSettings.BoardSize; i++)
            {
                for (int j = 0; j < GameSettings.BoardSize; j++)
                {
                    if (board.ContainsOpposingPiece(Square.At(i, j), colour.opposingPlayer()))
                    {
                        foreach (var move in board.GetPiece(Square.At(i, j)).GetAvailableMoves(board))
                        {
                            movesList.Add(new Square[] { Square.At(i, j), move });

                            if (board.ContainsOpposingPiece(move, colour))
                            {
                                aggressiveMovesList.Add(new Square[] { Square.At(i, j), move });
                            }
                        }
                    }
                }
            }

            if (aggressiveMovesList.Any())
                return aggressiveMovesList;

            return movesList;
        }

        public Square[] GetMove(Board board)
        {
            List<Square[]> movesList = GetAllMoves(board);
            if (movesList.Count != 0)
                return movesList[rnd.Next(0, movesList.Count)];
            return null;
        }
    }
}
