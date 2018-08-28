using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Checkers.Models
{
    public class EndGameDTO: OutgoingMessage
    {
        public Player winner { get; set; }
        public EndReason reason { get; set; }
    }
    
}
