using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Grabbit
{
    public class EventMessage
    {
        public string Body { get; set; }
        public string RoutingKey { get; set; }
        public string Topic { get; set; }
    }
}
