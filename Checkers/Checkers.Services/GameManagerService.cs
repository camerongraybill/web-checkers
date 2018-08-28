using Checkers.DAL;
using Checkers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Services
{
    public static class GameManagerService
    {

        public static StartGameDTO StartGame(Guid gameId, Player player)
        {
            Game game = GameRepository.Instance.getGame(gameId);
            return new StartGameDTO()
            {
                board = game.board,
                player = player
            };
        }


        /// <summary>
        /// If it is the players turn it returns a TurnDTO with the available moves
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        public static OutgoingMessage StartTurn(Guid gameId, Player player)
        {
            Game game = GameRepository.Instance.getGame(gameId);

            if (AvailableMoveService.IsGameOver(game.board))
            {
                return new EndGameDTO()
                {
                    reason = EndReason.GAME_OVER,
                    winner = AvailableMoveService.GetWinner(game.board)
                };
            }

            if (game.turn != player)
            {
                throw new ArgumentOutOfRangeException("It is not " + player.ToString() + "'s Turn.");
            }

            return new TurnDTO()
            {
                board = game.board,
                moves = AvailableMoveService.GetMoves(game.board, game.turn)
            };
        }


        /// <summary>
        /// Applies the move to the board. If there are mandatory moves it returns a list containing it. 
        /// If the turn is over, it returns a turnDTO with an empty list of moves
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="move"></param>
        /// <returns></returns>
        public static OutgoingMessage TakeTurn(Guid gameId, ActionDTO action)
        {
            //---Apply Move---
            Game game = GameRepository.Instance.getGame(gameId);
            Move move = new Move()
            {
                MoveTo = action.moveTo,
                Piece = game.board.Get(action.moveFrom.Item1, action.moveFrom.Item2)
            };

            if (!AvailableMoveService.GetMoves(game.board, game.turn).Contains(move))
            {
                throw new ArgumentOutOfRangeException("Invalid move");
            }

            game.board.ApplyMove(move);

            //---Check for Game Over---
            if (AvailableMoveService.IsGameOver(game.board))
            {
                return new EndGameDTO()
                {
                    reason = EndReason.GAME_OVER,
                    winner = AvailableMoveService.GetWinner(game.board)
                };
            }

            //---Check for forced jump moves---
            var jumpMoves = AvailableMoveService.GetJumpMoves(game.board, move.Piece);

            if (jumpMoves.Count == 0)
            {
                game.turn = (game.turn == Player.BLACK) ? Player.RED : Player.BLACK;
            }

            GameRepository.Instance.update(gameId, game);

            return new TurnDTO()
            {
                board = game.board,
                moves = jumpMoves
            };
        }

    }
}
