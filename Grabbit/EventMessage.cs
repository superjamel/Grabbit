using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Grabbit
{
    public class EventMessage
    {
        public string Body { get; set; }
        public string RoutingKey { get; set; }


        /// <summary>
        /// Converts this EventMessage to a byte array.
        /// </summary>
        /// <returns></returns>
        public byte[] ToByteArray()
        {
            string jsonValue = JsonConvert.SerializeObject(this);
            byte[] byteValue = Encoding.UTF8.GetBytes(jsonValue);
            return byteValue;
        }
    }
}
