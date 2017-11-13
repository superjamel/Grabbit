using Grabbit.EventProcessing.EventInterfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grabbit.EventProcessing.EventImplementations
{
    public class RabbitMQEventPublisher : IEventPublisher
    {
        private IModel Model { get; set; }

        public RabbitMQEventPublisher(IModel model)
        {
            Model = model;
        }
        public Task PublishEventAsync(EventMessage eventBody)
        {
           
            throw new NotImplementedException();
        }

    }
}
