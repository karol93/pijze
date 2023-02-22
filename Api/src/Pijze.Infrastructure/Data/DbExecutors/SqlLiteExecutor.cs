using System.Collections.Immutable;
using System.Data;
using Microsoft.Data.Sqlite;

namespace Pijze.Infrastructure.Data.DbExecutors;

public class SqlLiteExecutor : IDbExecutor
{
    private readonly IDbConnection _sqlConnection;
    
    public SqlLiteExecutor(IDbConnection sqlConnection) => _sqlConnection = sqlConnection;

    public async Task<T?> ExecuteScalar<T>(string sql, object? param = null)
    {
        await using var command = (SqliteCommand)_sqlConnection.CreateCommand();
        PrepareCommand(sql, param, command);
        return (T?)await command.ExecuteScalarAsync();
    }

    public async Task<IEnumerable<T>> Query<T>(
        string sql,
        object? param = null) where T : class
    {
        await using var command = (SqliteCommand)_sqlConnection.CreateCommand();
        PrepareCommand(sql, param, command);
        await using var reader = await command.ExecuteReaderAsync();
        return await reader.Query<T>();
    }

    public async Task<T?> QueryFirstOrDefault<T>(string sql, object? param = null) where T : class
    {
        await using var command = (SqliteCommand)_sqlConnection.CreateCommand();
        PrepareCommand(sql, param, command);
        await using var reader = await command.ExecuteReaderAsync();
        return await reader.FirstOrDefault<T?>();
    }

    public void Dispose() => _sqlConnection.Dispose();

    private static void PrepareCommand(string sql, object? param, SqliteCommand command)
    {
        command.CommandText = sql;
        command.CommandType = CommandType.Text;

        foreach (var kvp in DictionaryFromAnonymousObject(param))
        {
            command.Parameters.AddWithValue($"@{kvp.Key}",
                Guid.TryParse(kvp.Value?.ToString(), out var g) ? g.ToString() : kvp.Value);
        }
    }
    
    private static IDictionary<string, object?> DictionaryFromAnonymousObject(object? o)
    {
        var properties = o?.GetType().GetProperties();
        if (properties is null || !properties.Any())
            return ImmutableDictionary<string,object?>.Empty;

        IDictionary<string, object?> dic = new Dictionary<string, object?>();
        foreach (var prop in properties)
            dic.Add(prop.Name, prop.GetValue(o, null));
        return dic;
    }


}