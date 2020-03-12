using AGTec.Common.CQRS.Commands;
using System.Threading.Tasks;

namespace AGTec.Common.CQRS.Dispatchers
{
    public interface ICommandDispatcher
    {
        Task Execute<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
