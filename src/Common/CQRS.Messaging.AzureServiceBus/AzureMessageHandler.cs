using AGTec.Common.Base.Extensions;
using AGTec.Common.CQRS.Exceptions;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AGTec.Common.CQRS.Messaging.AzureServiceBus
{
    public class AzureMessageHandler : IMessageHandler
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMessageBusConfiguration _configuration;
        private readonly IMessageSerializer _serializer;
        private readonly IAzureMessageFilterFactory _filterFactory;
        private readonly ILogger<AzureMessageHandler> _logger;

        private ISubscriptionClient _subscriptionClient;

        public AzureMessageHandler(IServiceProvider serviceProvider,
            IMessageBusConfiguration configuration,
            IMessageSerializer serializer,
            IAzureMessageFilterFactory filterFactory,
            ILogger<AzureMessageHandler> logger)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
            _serializer = serializer;
            _filterFactory = filterFactory;
            _logger = logger;
        }

        public void Handle(string topicName, string subscriptionName, IEnumerable<IMessageFilter> filters = null)
        {
            _subscriptionClient =
                new SubscriptionClient(_configuration.ConnectionString, topicName, subscriptionName);

            var existingFilters = _subscriptionClient.GetRulesAsync().Result;

            existingFilters?.ForEach(filter => _subscriptionClient.RemoveRuleAsync(filter.Name).Wait());

            filters?.ForEach(filter =>
            {
                if (filter.IsValid())
                    _subscriptionClient.AddRuleAsync(_filterFactory.Create(filter)).Wait();
            });

            _subscriptionClient.RegisterMessageHandler(
                (message, cancellationToken) =>
                {
                    if (message.ContentType.Equals(_serializer.ContentType) == false)
                        throw new SerializerContentTypeMismatch();

                    _logger.LogInformation($"Processing message {message.MessageId}.");

                    var messageBody = _serializer.Deserialize(message.Body);

                    // Needs its own DI Scope
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var processor = scope.ServiceProvider.GetService<IMessageProcessor>();
                        processor.Process(messageBody).Wait(cancellationToken);
                    }

                    return Task.CompletedTask;
                },
                (exceptionEvent) =>
                {
                    var exceptionReceivedContext = $"Action: {exceptionEvent.ExceptionReceivedContext.Action} -" +
                                                   $"ClientId: {exceptionEvent.ExceptionReceivedContext.ClientId} -" +
                                                   $"Endpoint: {exceptionEvent.ExceptionReceivedContext.Endpoint} -" +
                                                   $"EntityPath: {exceptionEvent.ExceptionReceivedContext.EntityPath}.";

                    var errorMessage =
                        $"Error processing messages from {topicName}/{subscriptionName}. {exceptionReceivedContext}";

                    _logger.LogError(exceptionEvent.Exception, errorMessage);
                    throw new ErrorHandlingMessageException(errorMessage, exceptionEvent.Exception);
                });

        }
    }
}
