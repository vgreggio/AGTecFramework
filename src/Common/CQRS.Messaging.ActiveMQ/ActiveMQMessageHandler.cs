using AGTec.Common.Base.Extensions;
using AGTec.Common.CQRS.Exceptions;
using Apache.NMS;
using Apache.NMS.ActiveMQ.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGTec.Common.CQRS.Messaging.ActiveMQ
{
    public class ActiveMQMessageHandler : IMessageHandler, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMessageBusConfiguration _configuration;
        private readonly IMessageSerializer _serializer;
        private readonly IActiveMQMessageFilterFactory _filterFactory;
        private readonly ILogger<ActiveMQMessageHandler> _logger;
        private readonly IConnectionFactory _connectionFactory;

        private IConnection _connection;
        private ISession _session;
        private IMessageConsumer _consumer;

        public ActiveMQMessageHandler(IServiceProvider serviceProvider,
           IMessageBusConfiguration configuration,
           IMessageSerializer serializer,
           IActiveMQMessageFilterFactory filterFactory,
           ILogger<ActiveMQMessageHandler> logger)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
            _serializer = serializer;
            _filterFactory = filterFactory;
            _logger = logger;

            _connectionFactory = new NMSConnectionFactory(_configuration.ConnectionString);
        }

        public void Handle(string topicName, string subscriptionName, IEnumerable<IMessageFilter> filters = null)
        {
            var selector = buildSelector(filters);

            _connection = _connectionFactory.CreateConnection();
            _session = _connection.CreateSession();
            _consumer = _session.CreateDurableConsumer(new ActiveMQTopic(topicName), subscriptionName, selector, false);

            _consumer.Listener += (message) =>
            {
                if (message is IBytesMessage byteMessage)
                {
                    var contentType = byteMessage.Properties.GetString(ActiveMQConstants.Message.Properties.ContentType);
                    if (String.IsNullOrWhiteSpace(contentType))
                        throw new SerializerContentTypeMismatch($"Missing Content-type for message: {message.NMSCorrelationID}");

                    if (contentType.Equals(_serializer.ContentType) == false)
                        throw new SerializerContentTypeMismatch();

                    _logger.LogInformation($"Processing message {byteMessage.NMSCorrelationID}.");

                    var byteMessageBody = new byte[byteMessage.BodyLength];
                    byteMessage.ReadBytes(byteMessageBody);
                    var messageBody = _serializer.Deserialize(byteMessageBody);

                    // Needs its own DI Scope
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var processor = scope.ServiceProvider.GetService<IMessageProcessor>();
                        processor.Process(messageBody);
                    }
                }
                else
                    throw new MessageTypeNotSupportedException($"ActiveMQ message type: {message.NMSType} is not supported.");
            };
        }

        private String buildSelector(IEnumerable<IMessageFilter> filters = null)
        {
            if (filters == null || filters.IsNullOrEmpty())
                return String.Empty;

            StringBuilder selectorStringBuilder = new StringBuilder();
            filters?.ForEach(filter =>
            {
                if (filter.IsValid())
                {
                    if (selectorStringBuilder.ToString().IsNullOrEmpty() == false)
                        selectorStringBuilder.Append(" AND ");
                    selectorStringBuilder.Append(_filterFactory.Create(filter));
                }
            });

            return selectorStringBuilder.ToString();
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
                _consumer.Dispose();
                _session.Dispose();
                _connection.Dispose();
            }
        }
        #endregion
    }
}
