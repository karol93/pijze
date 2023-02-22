using Pijze.Domain.SeedWork;

namespace Pijze.Infrastructure.Data.EntityFramework;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly PijzeDbContext _context;

    public UnitOfWork(PijzeDbContext context) => _context = context;

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}