using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AGTec.Microservice.BackgroundServices
{
    public abstract class BackgroundService<T> : IHostedService, IDisposable
    {
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();

        protected abstract Task<T> ExecuteAsync(CancellationToken stoppingToken);

        public virtual async Task StartAsync(CancellationToken cancellationToken)
        {
            await ExecuteAsync(_stoppingCts.Token);
        }

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.Run(() => _stoppingCts.Cancel(), cancellationToken);
        }

        public virtual void Dispose()
        {
            _stoppingCts.Cancel();
        }
    }
}
