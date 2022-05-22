using Pijze.Application.Common.Commands;
using Pijze.Infrastructure.Caching;

namespace Pijze.Infrastructure.Commands;

internal class CachingCommandHandlerDecorator<T> : ICommandHandler<T> where T : class, ICommand
{
    private readonly ICommandHandler<T> _handler;
    private readonly ICacheStore _cache;

    public CachingCommandHandlerDecorator(ICommandHandler<T> handler, ICacheStore cache)
    {
        _handler = handler;
        _cache = cache;
    }

    public async Task HandleAsync(T command)
    {
        await _handler.HandleAsync(command);
        _cache.RemoveAll();
    }
}