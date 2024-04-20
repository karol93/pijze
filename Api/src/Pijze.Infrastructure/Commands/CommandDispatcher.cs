using Microsoft.Extensions.DependencyInjection;
using Pijze.Application.Common.Commands;

namespace Pijze.Infrastructure.Commands;

internal sealed class CommandDispatcher(IServiceProvider serviceProvider) : ICommandDispatcher
{
    public async Task SendAsync<TCommand>(TCommand command) where TCommand : class, ICommand
    {
        using var scope = serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();
        await handler.HandleAsync(command);
    }

    public async Task<TResult> SendAsync<TCommand, TResult>(TCommand command) where TCommand : class, ICommand<TResult>
    {
        using var scope = serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand,TResult>>();
        var result = await handler.HandleAsync(command);
        return result;
    }
}
