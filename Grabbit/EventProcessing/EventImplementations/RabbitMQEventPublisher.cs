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
        public Task PublishEventAsync(EventMessage eventBody,string routingKey)
        {
           
            throw new NotImplementedException();
        }

    }
}
