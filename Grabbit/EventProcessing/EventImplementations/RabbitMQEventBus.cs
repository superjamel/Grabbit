using Grabbit.EventProcessing.EventInterfaces;
using RabbitMQ.Client;
using System;
using System.Text;
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

        public void BasicPublish(EventMessage eventBody)
        {
            var topic = eventBody.Topic;
            var routingKey = eventBody.RoutingKey;
            Channel.ExchangeDeclare(topic, "topic");

            Channel.BasicPublish(exchange: topic,
                routingKey: routingKey,
                basicProperties: null,
                body: eventBody.BodyToByteArray());
        }

        public void BasicConsume(string topic, string routing, Action<EventMessage> callback)
        {
            var queueName = Channel.QueueDeclare().QueueName;
            Channel.QueueBind(queueName, topic, routing);

            var consumer = new EventingBasicConsumer(Channel);
            consumer.Received += (model, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body);
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

