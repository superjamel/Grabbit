using Grabbit.EventProcessing.EventInterfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grabbit.EventProcessing.EventImplementations
{
    public class RabbitMQEventPublisher : IEventPublisher
    {
        public Task PublishEventAsync(object eventBody,string routingKey)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Converts the given object to a byte.
        /// </summary>
        /// <returns></returns>
        private byte[] ConvertObjectToByte(object objectToConvert)
        {
            string jsonValue = JsonConvert.SerializeObject(objectToConvert);
            byte[] byteValue = Encoding.UTF8.GetBytes(jsonValue);
            return byteValue;
        }
    }
}
