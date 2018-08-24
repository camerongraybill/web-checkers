using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Models
{
    public class Move
    {
        public Piece Piece { get; set; }
        public Tuple<int, int> MoveTo { get; set; }
    }
}
