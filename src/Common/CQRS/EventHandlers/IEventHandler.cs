using AGTec.Common.CQRS.Events;
using System;
using System.Threading.Tasks;

namespace AGTec.Common.CQRS.EventHandlers
{
    public interface IEventHandler<in TEvent> : IDisposable where TEvent : IEvent
    {
        Task Process(TEvent evt);
    }
}
