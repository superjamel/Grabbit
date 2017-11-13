using System;
using System.Collections.Generic;
using System.Text;

namespace Grabbit.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class EventAttribute : Attribute
    {
        private string Routing;

        public EventAttribute(string routing)
        {
            Routing = routing;
        }
    }
}
