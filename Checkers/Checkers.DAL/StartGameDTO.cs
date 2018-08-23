using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Models
{
    public class StartGameDTO
    {
        Player player { get; set; }
        Board board { get; set; }
    }
}
