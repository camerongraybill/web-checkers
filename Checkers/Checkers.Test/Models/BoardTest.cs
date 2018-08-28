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

        [Fact]
        public void applyMoveTest()
        {
            Board board = Board.GetStartingBoard();

            board.ApplyMove(new Move()
            {
                MoveTo = Tuple.Create(2, 3),
                Piece = board.Get(1, 2)
            });

            Assert.Null(board.Get(1, 2));
            Assert.True(board.Get(2, 3).Player == Player.BLACK);
        }


        [Fact]
        public void applyMoveJumpTest()
        {
            Board board = new Board();

            board.Set(4, 5, new Piece()
            {
                Location = Tuple.Create(4, 5),
                Player = Player.RED,
                IsKing = false
            });

            board.Set(3, 4, new Piece()
            {
                Location = Tuple.Create(3, 4),
                Player = Player.BLACK,
                IsKing = false
            });

            board.ApplyMove(new Move()
            {
                MoveTo = Tuple.Create(2, 3),
                Piece = board.Get(4, 5)
            });

            Assert.Null(board.Get(3, 4));
            Assert.True(board.Get(2, 3).Player == Player.RED);
            Assert.Single(board.Pieces.FindAll(x => x != null));
        }

        [Fact]
        public void applyMoveJump2Test()
        {
            Board board = new Board();

            board.Set(3, 4, new Piece()
            {
                Location = Tuple.Create(3, 4),
                Player = Player.RED,
                IsKing = false
            });

            board.Set(4, 3, new Piece()
            {
                Location = Tuple.Create(4, 3),
                Player = Player.BLACK,
                IsKing = false
            });

            board.ApplyMove(new Move()
            {
                MoveTo = Tuple.Create(2, 5),
                Piece = board.Get(4, 3)
            });

            Assert.Null(board.Get(3, 4));
            Assert.True(board.Get(2, 5).Player == Player.BLACK);
            Assert.Single(board.Pieces.FindAll(x => x != null));
        }

    }
}
