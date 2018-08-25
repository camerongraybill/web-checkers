using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Models
{
    public class EndGameDTO: IOutgoingMessage
    {
        public Player winner { get; set; }
        public EndReason reason { get; set; }
    }
}
