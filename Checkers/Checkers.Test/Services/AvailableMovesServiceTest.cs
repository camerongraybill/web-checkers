using Checkers.Models;
using Checkers.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Checkers.Test.Services
{
    public class AvailableMovesServiceTest
    {
        [Fact]
        public void startingBoardAvailableMovesBlackTest()
        {
            var board = Board.GetStartingBoard();
            var moves = AvailableMoveService.GetMoves(board, Player.BLACK);

            Assert.Equal(7, moves.Count);
            Assert.Equal(7, moves.FindAll(x => x.Piece.Player == Player.BLACK).Count);
            Assert.Empty(moves.FindAll(x => x.Piece.Player == Player.RED));
            Assert.Equal(7, moves.FindAll(x => x.Piece.Location.Item2 == 2).Count); //assert all in row 2
            Assert.Equal(7, moves.FindAll(x => x.MoveTo.Item2 == 3).Count); //assert all move to row 3
        }

        [Fact]
        public void startingBoardAvailableMovesRedTest()
        {
            var board = Board.GetStartingBoard();
            var moves = AvailableMoveService.GetMoves(board, Player.RED);

            Assert.Equal(7, moves.Count);
            Assert.Equal(7, moves.FindAll(x => x.Piece.Player == Player.RED).Count);
            Assert.Empty(moves.FindAll(x => x.Piece.Player == Player.BLACK));
            Assert.Equal(7, moves.FindAll(x => x.Piece.Location.Item2 == 5).Count); //assert all in row 5
            Assert.Equal(7, moves.FindAll(x => x.MoveTo.Item2 == 4).Count); //assert all move to row 4
        }

        [Fact]
        public void startingBoardJumpTest()
        {
            var board = Board.GetStartingBoard();
            var moves = AvailableMoveService.GetJumpMoves(board, board.Get(2, 1));
            Assert.Empty(moves);
        }


        [Fact]
        public void blackJumpTest()
        {
            var board = new Board();
            board.Set(3, 2, new Piece()
            {
                IsKing = false,
                Location = Tuple.Create(3, 2),
                Player = Player.BLACK
            });

            board.Set(4, 3, new Piece()
            {
                IsKing = false,
                Location = Tuple.Create(4, 3),
                Player = Player.RED
            });

            var jumpMoves = AvailableMoveService.GetJumpMoves(board, new Piece()
            {
                IsKing = false,
                Location = Tuple.Create(3, 2),
                Player = Player.BLACK
            });

            Assert.Single(jumpMoves);
            Assert.Single(jumpMoves.FindAll(x => x.MoveTo.Item1 == 5 && x.MoveTo.Item2 == 4));

            var moves = AvailableMoveService.GetMoves(board, Player.BLACK);
            Assert.Equal(2, moves.Count);
        }
    }
}
