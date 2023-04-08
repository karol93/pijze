namespace Pijze.Infrastructure.Data.DbExecutors;

internal interface IDbExecutor : IDisposable
{
    Task<T?> ExecuteScalar<T>(
        string sql,
        object? param = null);
    
    Task<IEnumerable<T>> Query<T>(
        string sql,
        object? param = null) where T : class;
    
    public Task<T?> QueryFirstOrDefault<T>(
        string sql,
        object? param = null) where T : class;
}