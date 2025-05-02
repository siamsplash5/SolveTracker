using Dapper;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace SolveTracker.Infrastructure.DBContext;

public class DapperDBContext : IDapperDBContext
{
    private readonly string _connectionString = ConfigurationManager.AppSettings["ConnectionString"];

    public async Task ExecuteAsync<TParam>(TParam obj, string spName)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        await connection.ExecuteAsync(spName, obj, commandType: CommandType.StoredProcedure);
    }

    public async Task<TResult> GetInfoAsync<TParam, TResult>(TParam obj, string spName)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        return await connection.QueryFirstOrDefaultAsync<TResult>(spName, obj, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<TResult>> GetInfoListAsync<TParam, TResult>(TParam obj, string spName)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        return await connection.QueryAsync<TResult>(spName, obj, commandType: CommandType.StoredProcedure);
    }
}
