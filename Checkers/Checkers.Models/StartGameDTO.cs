using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Models
{
    public class StartGameDTO: IOutgoingMessage
    {
        public Player player { get; set; }
        public Board board { get; set; }
    }
}
