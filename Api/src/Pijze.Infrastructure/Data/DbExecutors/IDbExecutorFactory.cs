namespace Pijze.Infrastructure.Data.DbExecutors;

internal interface IDbExecutorFactory
{
    Task<IDbExecutor> CreateExecutor();
}