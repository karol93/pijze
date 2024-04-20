using Pijze.Application.Common.Commands;

namespace Pijze.Infrastructure.Commands;

public interface ICommandDispatcher
{
    Task SendAsync<TCommand>(TCommand command) where TCommand : class, ICommand;
    Task<TResult> SendAsync<TCommand,TResult>(TCommand command) where TCommand : class, ICommand<TResult>;
}
