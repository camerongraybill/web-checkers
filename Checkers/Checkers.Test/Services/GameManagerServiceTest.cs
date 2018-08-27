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
            var actual = GameManagerService.StartGame(id, Player.BLACK);
            Assert.Equal(Player.BLACK, actual.player);

        }

    }
}
