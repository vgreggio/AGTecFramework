using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Formatting.Elasticsearch;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using AGTec.Common.BackgroundTaskQueue;

namespace AGTec.Microservice
{
    public static class HostBuilderFactory
    {
        public static IHostBuilder CreateHostBuilder<TStartup>(string[] args) where TStartup : class
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile("k8s/appsettings.k8s.json", optional: true);
                })
                .UseSerilog((context, config) =>
                {
                    if (context.HostingEnvironment.IsDevelopment())
                    {
                        config.MinimumLevel
                        .Debug()
                        .Enrich
                        .FromLogContext()
                        .WriteTo
                        .File("log.txt", rollingInterval: RollingInterval.Day);
                    }
                    else
                    {
                        config.MinimumLevel
                        .Information()
                        .Enrich
                        .FromLogContext()
                        .WriteTo
                        .File(new ElasticsearchJsonFormatter(), "log.txt", rollingInterval: RollingInterval.Day);
                    }
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<TStartup>();
                })
                .ConfigureServices(services => services.AddHostedService<QueuedHostedService>());
        }
    }
}