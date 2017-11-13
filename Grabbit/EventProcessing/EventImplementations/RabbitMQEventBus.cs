using Grabbit.EventProcessing.EventInterfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;

namespace Grabbit.EventProcessing.EventImplementations
{
    public class RabbitMqEventBus : IEventBus
    {
        private IModel Channel { get; }

        public RabbitMqEventBus(IModel channel)
        {
            Channel = channel;
        }
        public void PublishEventAsync(EventMessage eventBody)
        {
            throw new NotImplementedException();
        }

        public void ConsumeEvent(string topic, string routing, Action<EventMessage> callback)
        {
            var queueName = Channel.QueueDeclare().QueueName;
            Channel.QueueBind(queueName, topic, routing);

            var consumer = new EventingBasicConsumer(Channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                callback.Invoke(new EventMessage()
                {
                    Body = message,
                    RoutingKey = ea.RoutingKey,
                    Topic = ea.Exchange
                });
            };

            Channel.BasicConsume(queue: queueName,
                autoAck: true,
                consumer: consumer);
        }
    }
}

