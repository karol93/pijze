using Pijze.Application.Common.Commands;
using Pijze.Infrastructure.Caching;

namespace Pijze.Infrastructure.Commands;

internal class CachingCommandHandlerDecorator<T>(ICommandHandler<T> handler, ICacheStore cache) : ICommandHandler<T>
    where T : class, ICommand
{
    public async Task HandleAsync(T command)
    {
        await handler.HandleAsync(command);
        cache.RemoveAll();
    }
}

internal class CachingCommandHandlerWithResultDecorator<T, TR>(ICommandHandler<T, TR> handler, ICacheStore cache)
    : ICommandHandler<T, TR>
    where T : class, ICommand<TR>
{
    public async Task<TR> HandleAsync(T command)
    {
        var result = await handler.HandleAsync(command);
        cache.RemoveAll();
        return result;
    }
}