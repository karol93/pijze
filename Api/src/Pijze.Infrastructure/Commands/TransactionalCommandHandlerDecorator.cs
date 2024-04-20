using Pijze.Application.Common.Commands;
using Pijze.Domain.SeedWork;

namespace Pijze.Infrastructure.Commands;

internal class TransactionalCommandHandlerDecorator<T>(
    ICommandHandler<T> handler,
    IUnitOfWork uow) : ICommandHandler<T>
    where T : class, ICommand
{
    public async Task HandleAsync(T command)
    {
        await handler.HandleAsync(command);
        await uow.SaveChangesAsync();
    }
}

internal class TransactionalCommandHandlerWithResultDecorator<T, TR>(
    ICommandHandler<T, TR> handler,
    IUnitOfWork uow) : ICommandHandler<T, TR>
    where T : class, ICommand<TR>
{
    public async Task<TR> HandleAsync(T command)
    {
        var result = await handler.HandleAsync(command);
        await uow.SaveChangesAsync();
        return result;
    }
}