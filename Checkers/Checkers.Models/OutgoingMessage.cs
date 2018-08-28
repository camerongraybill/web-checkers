using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Checkers.Models
{
    public class OutgoingMessage
    {
        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
