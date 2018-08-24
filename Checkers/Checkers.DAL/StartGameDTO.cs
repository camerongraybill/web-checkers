using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Models
{
    public class StartGameDTO
    {
        public Player player { get; set; }
        public Board board { get; set; }
    }
}
