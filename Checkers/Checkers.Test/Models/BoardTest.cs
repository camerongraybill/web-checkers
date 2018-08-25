using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Checkers.Models;


namespace Checkers.Test.Models
{
    public class BoardTest
    {
        [Fact]
        public void getEmptyTest()
        {
            Board board = new Board();
            Assert.True(board.Get(0, 0) == null);
        }


        [Fact]
        public void createStartingBoardTest()
        {
            Board board = Board.GetStartingBoard();

            Assert.Equal(24, board.Pieces.FindAll(x => x != null).Count);

            Assert.True(board.Get(0, 0) == null);
            Assert.True(board.Get(0, 1).Player == Player.BLACK);
            Assert.True(board.Get(0, 2) == null);
            Assert.True(board.Get(0, 3) == null);
            Assert.True(board.Get(0, 4) == null);
            Assert.True(board.Get(0, 5).Player == Player.RED);
            Assert.True(board.Get(0, 6) == null);
            Assert.True(board.Get(0, 7).Player == Player.RED);
        }

    }
}
