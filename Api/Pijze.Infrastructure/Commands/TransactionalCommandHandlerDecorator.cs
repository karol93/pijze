using Pijze.Application.Common.Commands;
using Pijze.Domain.SeedWork;

namespace Pijze.Infrastructure.Commands;

internal class TransactionalCommandHandlerDecorator<T> : ICommandHandler<T> where T : class, ICommand
{
    private readonly ICommandHandler<T> _handler;
    private readonly IUnitOfWork _uow;

    public TransactionalCommandHandlerDecorator(ICommandHandler<T> handler,
        IUnitOfWork uow)
    {
        _handler = handler;
        _uow = uow;
    }

    public async Task HandleAsync(T command)
    {
        await _handler.HandleAsync(command);
        await _uow.SaveChangesAsync();
    }
}