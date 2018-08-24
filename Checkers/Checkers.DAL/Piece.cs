using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Models
{
    public class Piece
    {
        public Tuple<int, int> Location { get; set; }
        public Player Player { get; set; }
        public bool IsKing { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (this.GetType() != obj.GetType()) return false;

            Piece p = (Piece)obj;
            return Location.Equals(p.Location) && Player.Equals(p.Player) && IsKing == p.IsKing;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Location, Player, IsKing);
        }
    }
}
