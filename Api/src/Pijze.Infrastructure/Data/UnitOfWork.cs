using Pijze.Domain.SeedWork;
using System.Threading.Tasks;

namespace Pijze.Infrastructure.Data;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly PijzeDbContext _context;

    public UnitOfWork(PijzeDbContext context) => _context = context;

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}