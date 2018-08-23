using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Models
{
    class ActionDTO
    {
        Tuple<int, int> moveFrom { get; set; }
        Tuple<int, int> moveTo { get; set; }

    }
}
