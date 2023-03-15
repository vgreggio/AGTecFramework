﻿using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AGTec.Common.BackgroundTaskQueue
{
    public class QueuedHostedService : BackgroundService<int>
    {
        private const int THREAD_SLEEP_TIME_WHILE_PAUSED = 5000; // 5 seconds
        
        private readonly IBackgroundTaskQueue _tasksToRun;
        private readonly ILogger<QueuedHostedService> _logger;

        private int _tasksCounter;

        public QueuedHostedService(IBackgroundTaskQueue tasksToRun,
            ILogger<QueuedHostedService> logger)
        {
            _tasksToRun = tasksToRun;
            _logger = logger;
        }

        protected override async Task<int> ExecuteAsync(CancellationToken stoppingToken)
        {
            while (stoppingToken.IsCancellationRequested == false)
            {
                while (!_tasksToRun.Paused && stoppingToken.IsCancellationRequested == false)
                {
                    var taskToRun = await _tasksToRun.Dequeue(stoppingToken);

                    try
                    {
                        _logger.LogInformation($"Starting '{taskToRun.Key}'.");

                        await taskToRun.Value(stoppingToken);

                        _logger.LogInformation($"Finished '{taskToRun.Key}'.");
                        _tasksCounter++;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Error running task {nameof(taskToRun.Key)}.", ex);
                    }
                }
                Thread.Sleep(THREAD_SLEEP_TIME_WHILE_PAUSED);
            }
            return _tasksCounter;
        }
    }
}
