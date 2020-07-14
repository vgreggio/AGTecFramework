using Microsoft.Extensions.DependencyInjection;

namespace AGTec.Common.CQRS.Messaging.JsonSerializer
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMessagingSerializer(this IServiceCollection services)
        {
            services.AddTransient<IMessageSerializer, JsonMessageSerializer>();
            services.AddTransient<IPayloadSerializer, JsonPayloadSerializer>();
            return services;
        }
    }
}
