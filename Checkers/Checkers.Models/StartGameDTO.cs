using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Checkers.Models
{
    public class StartGameDTO: OutgoingMessage
    {
        public Player player { get; set; }
        public Board board { get; set; }
    }
}
