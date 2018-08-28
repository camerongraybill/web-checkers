using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Checkers.Models
{
    public class Board
    {
        public List<Piece> Pieces { get; set; } = new List<Piece>(64);

        public Board()
        {
            Pieces.AddRange(Enumerable.Repeat((Piece)null, 64));
        }

        /// <summary>
        /// Applies a move to the board. Does not check that it is a valid move
        /// </summary>
        /// <param name="move">Move to be applied.</param>
        public void ApplyMove(Move move)
        {
            var boardPiece = Get(move.Piece.Location.Item1, move.Piece.Location.Item2);
            Debug.Assert(move.Piece.Equals(boardPiece));

            var loc = move.Piece.Location;
            var to = move.MoveTo;

            //----Jump Move----
            if (Math.Abs(loc.Item1 - to.Item1) == 2 &&
                Math.Abs(loc.Item2 - to.Item2) == 2)
            {
                //null the jumped piece
                Set(loc.Item1 + ((to.Item1 - loc.Item1) / 2), loc.Item2 + ((to.Item2 - loc.Item2) / 2), null);
            }


            //----Check Kings----
            if ((move.Piece.Player == Player.BLACK && to.Item2 == 7) ||
                (move.Piece.Player == Player.RED && to.Item2 == 0))
            {
                move.Piece.IsKing = true;
            }

            //set new location
            Set(to.Item1, to.Item2, new Piece()
            {
                IsKing = boardPiece.IsKing,
                Location = to,
                Player = boardPiece.Player
            });


            //null old location
            Set(loc.Item1, loc.Item2, null);
        }

        public Piece Get(int x, int y)
        {
            return Pieces[8 * y + x];
        }

        public void Set(int x, int y, Piece piece)
        {
            Pieces[8 * y + x] = piece;
        }

        public static Board GetStartingBoard()
        {
            Board board = new Board();
            //---Black Pieces---
            for (int y = 0; y < 3; y++)
            {
                for (int x = 1 - (y % 2); x < 8; x += 2)
                {
                    board.Set(x, y, new Piece()
                    {
                        IsKing = false,
                        Location = Tuple.Create(x, y),
                        Player = Player.BLACK
                    });
                }
            }

            //---Red Pieces---
            for (int y = 5; y < 8; y++)
            {
                for (int x = 1 - (y % 2); x < 8; x += 2)
                {
                    board.Set(x, y, new Piece()
                    {
                        IsKing = false,
                        Location = Tuple.Create(x, y),
                        Player = Player.RED
                    });
                }
            }
            return board;
        }
    }
}
