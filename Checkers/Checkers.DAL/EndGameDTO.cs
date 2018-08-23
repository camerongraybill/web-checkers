using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Models
{
    public class EndGameDTO
    {
        Player winner { get; set; }
        EndReason reason { get; set; }
    }
}
