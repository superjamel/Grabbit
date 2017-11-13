using Grabbit.EventProcessing.EventImplementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Grabbit.Test
{
    [TestClass]
    public class EventPublisherTest
    {
        [TestMethod]
        public void Consume_WhenMessageIsPublished_ExecuteMessage()
        {
            // Arrange
            var connectionFactory = new ConnectionFactory { HostName = "localhost" };
            var connection = connectionFactory.CreateConnection();
            var sut = new RabbitMqEventBus(connection.CreateModel());
            EventMessage caughtEvent = null;
            var waitHandle = new ManualResetEvent(false);

            // Act
            
            var channel = connection.CreateModel();
            sut.ConsumeEvent("log_topic", "*.log.*", eventMessage =>
            {
                caughtEvent = eventMessage;
                waitHandle.Set();
            });
            channel.BasicPublish(exchange: "log_topic",
                routingKey: "localhost.log.info",
                basicProperties: null,
                body: new EventMessage {Body = "test", RoutingKey = "test"}.ToByteArray());

            var isReceived = waitHandle.WaitOne(1000);

            // Assert
            Assert.IsTrue(isReceived);
            
            Assert.AreEqual("test", caughtEvent.Body);
            Assert.AreEqual("test", caughtEvent.RoutingKey);
        }
    }
}
