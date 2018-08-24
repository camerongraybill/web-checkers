using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Models
{
    public class ActionDTO
    {
        public Tuple<int, int> moveFrom { get; set; }
        public Tuple<int, int> moveTo { get; set; }

    }
}
