using Grabbit.EventProcessing.EventInterfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Grabbit.Extension;
namespace Grabbit.EventProcessing.EventImplementations
{
    public class RabbitMQEventPublisher : IEventPublisher
    {
        public Task PublishEventAsync(object eventBody,string routingKey)
        {
           
            throw new NotImplementedException();
        }

    }
}
