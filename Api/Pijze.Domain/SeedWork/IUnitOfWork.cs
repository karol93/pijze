namespace Pijze.Domain.SeedWork;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}