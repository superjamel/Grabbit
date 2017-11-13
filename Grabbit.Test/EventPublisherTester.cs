using Grabbit.EventProcessing.EventImplementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Grabbit.Test
{
    [TestClass]
    public class EventPublisherTester
    {
        [TestMethod]
        public void Consume_WhenMessageIsPublished_ExecuteMessage()
        {
            var connectionFactory = new ConnectionFactory { HostName = "localhost" };
            var connection = connectionFactory.CreateConnection();
            var sut = new RabbitMQEventPublisher(connection.CreateModel());
            EventMessage publishedEvent = new EventMessage
            {
                Body = "test",
                RoutingKey = "key"
            };
            var waitHandle = new ManualResetEvent(false);


            sut.PublishEventAsync(publishedEvent);
            var channel = connection.CreateModel();
            channel.BasicPublish(exchange: "",
                routingKey: "",
                basicProperties: null,
                body: Encoding.UTF8.GetBytes("test"));
            var isReceived = waitHandle.WaitOne(1000);

            Assert.IsTrue(isReceived);
            
            Assert.AreEqual("test", publishedEvent.Body);
            Assert.AreEqual("test", publishedEvent.RoutingKey);
        }
    }
}
