using Checkers.DAL;
using Checkers.Models;
using Checkers.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Checkers.Test.Services
{
    public class GameManagerServiceTest
    {

        [Fact]
        public void startGameTest()
        {
            Guid id = Guid.NewGuid();
            var actual = GameManagerService.StartGame(id, Player.BLACK, "");
            Assert.Equal(Player.BLACK, actual.player);
        }

        [Fact]
        public void startTurnGameOverTest()
        {
            Guid id = Guid.NewGuid();
            var board = new Board();
            board.Set(0, 0, new Piece()
            {
                IsKing = false,
                Location = Tuple.Create(0, 0),
                Player = Player.BLACK
            });

            GameRepository.Instance.update(id, new Game()
            {
                turn = Player.BLACK,
                board = board
            });

            var actual = GameManagerService.StartTurn(id, Player.BLACK);
            Assert.True(((EndGameDTO)actual).reason == EndReason.GAME_OVER);
            Assert.True(((EndGameDTO)actual).winner == Player.BLACK);
        }

        [Fact]
        public void startTurnWrongPlayerTest()
        {
            Guid id = Guid.NewGuid();
            var board = new Board();
            board.Set(1, 0, new Piece()
            {
                IsKing = false,
                Location = Tuple.Create(1, 0),
                Player = Player.BLACK
            });

            board.Set(3, 0, new Piece()
            {
                IsKing = true,
                Location = Tuple.Create(3, 0),
                Player = Player.RED
            });

            GameRepository.Instance.update(id, new Game()
            {
                turn = Player.BLACK,
                board = board
            });

            Assert.Throws<ArgumentOutOfRangeException>(() => GameManagerService.StartTurn(id, Player.RED));
        }

        [Fact]
        public void startTurnTest()
        {
            Guid id = Guid.NewGuid();
            var board = Board.GetStartingBoard();
            GameRepository.Instance.update(id, new Game()
            {
                turn = Player.BLACK,
                board = board
            });

            var actual = GameManagerService.StartTurn(id, Player.BLACK);
            Assert.True(((TurnDTO)actual).board.Equals(board));
            Assert.Equal(7, ((TurnDTO)actual).moves.Count);
        }


        [Fact]
        public void takeTurnTest()
        {
            Guid id = Guid.NewGuid();
            var board = Board.GetStartingBoard();
            GameRepository.Instance.update(id, new Game()
            {
                turn = Player.BLACK,
                board = board
            });

            var actual = GameManagerService.TakeTurn(id, new ActionDTO()
            {
                moveFrom = Tuple.Create(3, 2),
                moveTo = Tuple.Create(4, 3)
            });

            Assert.Null(((TurnDTO)actual).board.Get(3, 2));
            Assert.True(((TurnDTO)actual).board.Get(4, 3).Player == Player.BLACK);
            Assert.Empty(((TurnDTO)actual).moves);
        }

        [Fact]
        public void takeTurnEndGameTest()
        {
            Guid id = Guid.NewGuid();
            var board = new Board();
            board.Set(0, 0, new Piece()
            {
                IsKing = false,
                Location = Tuple.Create(0, 0),
                Player = Player.BLACK
            });

            GameRepository.Instance.update(id, new Game()
            {
                turn = Player.BLACK,
                board = board
            });

            var actual = GameManagerService.TakeTurn(id, new ActionDTO()
            {
                moveFrom = Tuple.Create(0, 0),
                moveTo = Tuple.Create(1, 1)
            });
            Assert.True(((EndGameDTO)actual).reason == EndReason.GAME_OVER);
            Assert.True(((EndGameDTO)actual).winner == Player.BLACK);
        }

        [Fact]
        public void getUserIDTest()
        {
            Guid id = Guid.NewGuid();
            var actual = GameManagerService.StartGame(id, Player.BLACK, "Adam");
            Assert.Equal(Player.BLACK, actual.player);

            Assert.Equal("Adam", GameManagerService.GetUserId(id, Player.BLACK));
        }

        [Fact]
        public void getGameIDTest()
        {
            Guid id = Guid.NewGuid();
            var actual = GameManagerService.StartGame(id, Player.BLACK, "Adam");
            Assert.Equal(Player.BLACK, actual.player);

            Assert.Equal(id, GameManagerService.GetGameId("Adam"));
        }

        [Fact]
        public void getColorTest()
        {
            Guid id = Guid.NewGuid();
            var actual = GameManagerService.StartGame(id, Player.BLACK, "Adam");
            Assert.Equal(Player.BLACK, actual.player);

            Assert.Equal(Player.BLACK, GameManagerService.GetColor("Adam"));
        }
    }
}
