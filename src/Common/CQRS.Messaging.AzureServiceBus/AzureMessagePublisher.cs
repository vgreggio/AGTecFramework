using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AGTec.Common.CQRS.Messaging.AzureServiceBus
{
    public sealed class AzureMessagePublisher : IMessagePublisher
    {
        private readonly IMessageBusConfiguration _configuration;
        private readonly IMessageSerializer _serializer;
        private readonly ILogger<AzureMessagePublisher> _logger;

        public AzureMessagePublisher(IMessageBusConfiguration configuration,
            IMessageSerializer serializer,
            ILogger<AzureMessagePublisher> logger)
        {
            _configuration = configuration;
            _serializer = serializer;
            _logger = logger;
        }

        public async Task Publish(string topicName, IMessage message)
        {
            var messagePayload = _serializer.Serialize(message);

            var topicClient = new TopicClient(_configuration.ConnectionString, topicName);

            var messageGuid = Guid.NewGuid().ToString();

            await topicClient.SendAsync(new Microsoft.Azure.ServiceBus.Message(messagePayload)
            {
                CorrelationId = message.Id.ToString(),
                MessageId = messageGuid,
                ContentType = _serializer.ContentType,
                Label = message.Label
            });

            await topicClient.CloseAsync();

            _logger.LogInformation($"Message {messageGuid} published to {topicName}.");
        }
    }
}
