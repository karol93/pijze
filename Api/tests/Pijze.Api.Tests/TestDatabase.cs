using System;
using Microsoft.EntityFrameworkCore;
using Pijze.Infrastructure.Data;

namespace Pijze.Api.Tests;

internal class TestDatabase : IDisposable
{
    public PijzeDbContext Context { get; }

    public TestDatabase()
    {
        Context = new PijzeDbContext(new DbContextOptionsBuilder<PijzeDbContext>().UseInMemoryDatabase("PijzeInMemory").Options);
    }

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}