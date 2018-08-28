using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Models
{
    public class TurnDTO: OutgoingMessage
    {
        public Board board { get; set; }
        public List<Move> moves { get; set; }
    }
}
