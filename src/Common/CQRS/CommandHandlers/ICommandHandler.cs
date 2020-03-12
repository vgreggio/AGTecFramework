using AGTec.Common.CQRS.Commands;
using System;
using System.Threading.Tasks;

namespace AGTec.Common.CQRS.CommandHandlers
{
    public interface ICommandHandler<in TCommand> : IDisposable where TCommand : ICommand
    {
        Task Execute(TCommand command);
    }
}
