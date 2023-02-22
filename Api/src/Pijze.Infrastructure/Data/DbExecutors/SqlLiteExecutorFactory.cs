using System.Data.SqlClient;
using Microsoft.Data.Sqlite;

namespace Pijze.Infrastructure.Data.DbExecutors;

internal class SqlLiteExecutorFactory : IDbExecutorFactory
{
    private readonly string _connectionString;

    public SqlLiteExecutorFactory(string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentNullException(nameof(connectionString));
        }

        _connectionString = connectionString;
    }
    
    public async Task<IDbExecutor> CreateExecutor()
    {
        var dbConnection = new SqliteConnection(_connectionString);
        await dbConnection.OpenAsync();

        return new SqlLiteExecutor(dbConnection);
    }
}