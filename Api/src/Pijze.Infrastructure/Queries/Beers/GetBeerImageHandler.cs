using Pijze.Application.Beers.Queries;
using Pijze.Application.Common.Queries;
using Pijze.Infrastructure.Data.DbExecutors;

namespace Pijze.Infrastructure.Queries.Beers;

internal class GetBeerImageHandler : IQueryHandler<GetBeerImage, byte[]?>
{
    private readonly IDbExecutorFactory _dbExecutorFactory;

    public GetBeerImageHandler(IDbExecutorFactory dbExecutorFactory)
    {
        _dbExecutorFactory = dbExecutorFactory;
    }

    public async Task<byte[]?> HandleAsync(GetBeerImage query)
    {
        using var db = await _dbExecutorFactory.CreateExecutor();
        var image = await db.ExecuteScalar<byte[]?>(
            "SELECT beer.Image FROM Beers beer where beer.Id = @beerId",
            new {beerId = query.BeerId});
        return image;
    }
}
