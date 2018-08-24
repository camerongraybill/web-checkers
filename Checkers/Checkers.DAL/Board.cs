using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Checkers.Models
{
    public class Board
    {
        private List<Piece> pieces;

        /// <summary>
        /// Applies a move to the board. Does not check that it is a valid move
        /// </summary>
        /// <param name="move">Move to be applied.</param>
        public void ApplyMove(Move move)
        {
            var boardPiece = Get(move.Piece.Location.Item1, move.Piece.Location.Item2);
            Debug.Assert(move.Piece.Equals(boardPiece));


            var loc = move.Piece.Location;
            Set(loc.Item1, loc.Item2, null);

            move.Piece.Location = move.MoveTo;
            Set(move.MoveTo.Item1, move.MoveTo.Item2, move.Piece);
        }

        public Piece Get(int x, int y)
        {
            return pieces[8 * y + x];
        }

        public void Set(int x, int y, Piece piece)
        {
            pieces[8 * y + x] = piece;
        }
    }
}
