using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Models
{
    public class Move
    {
        Piece Piece { get; set; }
        Tuple<int, int> MoveTo { get; set; }
    }
}
