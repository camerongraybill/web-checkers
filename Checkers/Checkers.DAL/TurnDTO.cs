using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Models
{
    public class TurnDTO
    {
        Board board { get; set; }
        List<Move> moves { get; set; }
    }
}
