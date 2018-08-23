using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Models
{
    public class Piece
    {
        Tuple<int, int> Location { get; set; }
        Player player { get; set; }
        bool isKing { get; set; }
    }
}
