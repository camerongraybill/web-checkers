using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Models
{
    public class Move
    {
        public Piece Piece { get; set; }
        public Tuple<int, int> MoveTo { get; set; }

        public override bool Equals(object obj)
        {
            var move = obj as Move;
            return move != null &&
                   EqualityComparer<Piece>.Default.Equals(Piece, move.Piece) &&
                   EqualityComparer<Tuple<int, int>>.Default.Equals(MoveTo, move.MoveTo);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Piece, MoveTo);
        }
    }
}
