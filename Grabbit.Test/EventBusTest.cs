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
            sut.BasicConsume("log_topic", "*.log.*", eventMessage =>
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

        [TestMethod]
        public void BasicPublish_EventMessage_ShouldPublishTheEventToRabbitMQ()
        {
            // Arrange
            var connectionFactory = new ConnectionFactory { HostName = "localhost" };
            var connection = connectionFactory.CreateConnection();
            var channel = connection.CreateModel();
            var topicName = TestContext.TestName + "-topic";
            channel.ExchangeDeclare(exchange: topicName,
                type: "topic");
            var queue = channel.QueueDeclare();
            channel.QueueBind(queue.QueueName, topicName, "#");
            var consumer = new EventingBasicConsumer(channel);
            BasicDeliverEventArgs actualEventArgs = null;
            var waitHandle = new ManualResetEvent(false);
            consumer.Received += (model, eventArgs) =>
            {
                actualEventArgs = eventArgs;
                waitHandle.Set();
            };
            channel.BasicConsume(queue.QueueName, autoAck: true, consumer: consumer);
            var sut = new RabbitMqEventBus(connection.CreateModel());

            // Act
            sut.BasicPublish(new EventMessage
            {
                Body = "the message",
                RoutingKey = TestContext.TestName,
                Topic = topicName
            });

            // Assert  
            var handled = waitHandle.WaitOne(1000);
            Assert.IsTrue(handled);
            Assert.AreEqual("the message", Encoding.UTF8.GetString(actualEventArgs.Body));
            Assert.AreEqual(topicName, actualEventArgs.Exchange);
            Assert.AreEqual(TestContext.TestName, actualEventArgs.RoutingKey);
        }
    }
}
