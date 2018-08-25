using System;
using System.Collections.Generic;
using System.Text;
using Checkers.Models;

namespace Checkers.Services
{
    public static class AvailableMoveService
    {
        /// <summary>
        /// Returns all available moves for a player
        /// </summary>
        /// <param name="board">Game board</param>
        /// <param name="player">Player whos turn it is</param>
        /// <returns></returns>
        public static List<Move> GetMoves(Board board, Player player)
        {
            //----Get Normal Moves----
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (board.Get(x, y) != null)
                    {

                    }

                }

            }

            return null;
        }

        /// <summary>
        /// Returns the mandatory jump moves for a piece
        /// </summary>
        /// <param name="board">GameBoard</param>
        /// <param name="piece">Piece to check for jumps on</param>
        /// <returns></returns>
        public static List<Move> GetJumpMoves(Board board, Piece piece)
        {
            return null;
        }

        public static bool IsGameOver(Board board)
        {
            return board.Pieces.FindAll(x => x.Player == Player.RED).Count == 0 ||
                board.Pieces.FindAll(x => x.Player == Player.BLACK).Count == 0;
        }

        /// <summary>
        /// Returns the winner if there is one. If there is no winner it returns black.
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public static Player GetWinner(Board board)
        {
            return board.Pieces.FindAll(x => x.Player == Player.RED).Count == 0 ? Player.RED : Player.BLACK;
        }

    }
}
