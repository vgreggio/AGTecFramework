using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AGTec.Common.Monitor;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAGTecMonitor(this IServiceCollection services,
        IHostEnvironment hostEnv)
    {
        if (hostEnv.IsDevelopment())
        {
            services.AddMiniProfiler(options =>
            {
                options.RouteBasePath = "/profiler";
                options.SqlFormatter = new StackExchange.Profiling.SqlFormatters.InlineFormatter();
            }).AddEntityFramework();
        }

        return services;
    }
}