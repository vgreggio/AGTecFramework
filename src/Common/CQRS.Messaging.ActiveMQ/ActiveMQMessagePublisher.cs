using Apache.NMS;
using Apache.NMS.ActiveMQ.Commands;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AGTec.Common.CQRS.Messaging.ActiveMQ
{
    public class ActiveMQMessagePublisher : IMessagePublisher, IDisposable
    {
        private readonly IMessageBusConfiguration _configuration;
        private readonly IMessageSerializer _serializer;
        private readonly ILogger<ActiveMQMessagePublisher> _logger;
        private readonly IConnectionFactory _connectionFactory;

        private IConnection _connection;
        private ISession _session;
        private IMessageProducer _producer;

        public ActiveMQMessagePublisher(IMessageBusConfiguration configuration,
            IMessageSerializer serializer,
            ILogger<ActiveMQMessagePublisher> logger)
        {
            _configuration = configuration;
            _serializer = serializer;
            _logger = logger;

            _connectionFactory = new NMSConnectionFactory(_configuration.ConnectionString);
        }
        public Task Publish(string topicName, IMessage message)
        {
            var messagePayload = _serializer.Serialize(message);

            _connection = _connectionFactory.CreateConnection();
            _session = _connection.CreateSession();
            _producer = _session.CreateProducer(new ActiveMQTopic(topicName));

            var messageGuid = Guid.NewGuid().ToString();

            var byteMessage = _session.CreateBytesMessage(messagePayload);
            byteMessage.NMSMessageId = messageGuid;
            byteMessage.NMSCorrelationID = message.Id.ToString();
            byteMessage.Properties.SetString(ActiveMQConstants.Message.Properties.ContentType, _serializer.ContentType);
            byteMessage.Properties.SetString(ActiveMQConstants.Message.Properties.Label, _serializer.ContentType);

            _producer.Send(byteMessage);

            _logger.LogInformation($"Message {messageGuid} published to {topicName}.");

            return Task.CompletedTask;
        }


        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                _producer.Dispose();
                _session.Dispose();
                _connection.Dispose();
            }
        }
        #endregion
    }
}
