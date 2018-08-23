using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Models
{
    class EndGameDTO
    {
        Player winner { get; set; }
        EndReason reason { get; set; }
    }
}
