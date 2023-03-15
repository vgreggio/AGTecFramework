using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AGTec.Common.BackgroundTaskQueue
{
    public interface IBackgroundTaskQueue
    {
        void Queue(String workName, Func<CancellationToken, Task> workItem);

        Task<KeyValuePair<string, Func<CancellationToken, Task>>> Dequeue(CancellationToken cancellationToken);
        
        IEnumerable<string> QueuedTasks { get; }

        bool Paused { get; }
        void Pause();
        void Restart();
    }
}
