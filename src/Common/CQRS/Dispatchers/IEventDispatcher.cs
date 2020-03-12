using AGTec.Common.CQRS.Events;
using System.Threading.Tasks;

namespace AGTec.Common.CQRS.Dispatchers
{
    public interface IEventDispatcher
    {
        Task Raise<TEvent>(TEvent evt) where TEvent : IEvent;
    }
}
