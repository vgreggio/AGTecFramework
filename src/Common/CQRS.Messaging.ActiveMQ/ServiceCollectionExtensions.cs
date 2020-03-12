﻿using AGTec.Common.CQRS.Dispatchers;
using AGTec.Common.CQRS.Messaging.JsonSerializer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AGTec.Common.CQRS.Messaging.ActiveMQ
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCQRSWithMessaging(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddCQRS(configuration);
            services.AddTransient<IEventDispatcher, EventDispatcher>();
            services.AddMessaging(configuration);
            return services;
        }

        private static IServiceCollection AddMessaging(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddTransient<IMessageSerializer, JsonMessageSerializer>();
            services.AddTransient<IPayloadSerializer, JsonPayloadSerializer>();
            services.AddTransient<IMessageProcessor, MessageProcessor>();

            // Adds ActiveMQ as MessageBroker.
            services.AddActiveMQMessaging(configuration);

            return services;
        }

        private static IServiceCollection AddActiveMQMessaging(this IServiceCollection services, IConfiguration configuration)
        {
            var activeMQConfiguration = configuration.GetSection(ActiveMQConfiguration.ConfigSectionName).Get<ActiveMQConfiguration>();

            if (activeMQConfiguration.IsValid() == false)
                throw new Exception($"Configuration section '{ActiveMQConfiguration.ConfigSectionName}' not found.");

            services.AddSingleton<IMessageBusConfiguration>(activeMQConfiguration);
            services.AddTransient<IActiveMQMessageFilterFactory, ActiveMQMessageFilterFactory>();
            services.AddTransient<IMessagePublisher, ActiveMQMessagePublisher>();
            services.AddTransient<IMessageHandler, ActiveMQMessageHandler>();

            return services;
        }
    }
}
