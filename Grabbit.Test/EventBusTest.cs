using Grabbit.EventProcessing.EventImplementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;

namespace Grabbit.Test
{
    [TestClass]
    public class EventBusTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void BasicConsume_WhenMessagePublished_ExecutesConsume()
        {
            // Arrange
            var connectionFactory = new ConnectionFactory { HostName = "localhost" };
            var connection = connectionFactory.CreateConnection();
            var sut = new RabbitMqEventBus(connection.CreateModel());
            EventMessage caughtEvent = null;
            var waitHandle = new ManualResetEvent(false);

            // Act
            sut.ConsumeEvent("log_topic", "*.log.*", eventMessage =>
            {
                caughtEvent = eventMessage;
                waitHandle.Set();
            });
            var channel = connection.CreateModel();
            channel.BasicPublish(exchange: "log_topic",
                routingKey: "localhost.log.info",
                basicProperties: null,
                body: Encoding.UTF8.GetBytes("message body"));
            var isReceived = waitHandle.WaitOne(1000);

            // Assert
            Assert.IsTrue(isReceived);
            Assert.AreEqual("message body", caughtEvent.Body);
            Assert.AreEqual("localhost.log.info", caughtEvent.RoutingKey);
            Assert.AreEqual("log_topic", caughtEvent.Topic);
        }
    }
}
