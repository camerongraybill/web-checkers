using Checkers.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Checkers.Test.Models
{
    public class GameTest
    {
        [Fact]
        public void startingGameTest()
        {
            Game game = Game.CreateStartingGame();

            Assert.Equal(Player.BLACK, game.turn);
            Assert.Equal(24, game.board.Pieces.FindAll(x => x != null).Count);
        }
    }
}
