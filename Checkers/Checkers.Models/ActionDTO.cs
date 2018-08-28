using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Checkers.Models
{
    public class ActionDTO
    {
        public Tuple<int, int> moveFrom { get; set; }
        public Tuple<int, int> moveTo { get; set; }

        public static ActionDTO Deserialize(string raw)
        {
            dynamic response = JObject.Parse(raw);
            return new ActionDTO()
            {
                moveFrom = Tuple.Create((int)response["from"][0], (int)response["from"][1]),
                moveTo = Tuple.Create((int)response["to"][0], (int)response["to"][1])
            };
        }

    }
}
