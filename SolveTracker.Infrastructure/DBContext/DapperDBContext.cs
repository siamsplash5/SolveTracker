﻿using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace SolveTracker.Infrastructure.DBContext;

public class DapperDBContext(IConfiguration configuration) : IDapperDBContext
{
    private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection");

    public async Task ExecuteAsync<TParam>(TParam obj, string spName)
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();
        await connection.ExecuteAsync(spName, obj, commandType: CommandType.StoredProcedure);
    }

    public async Task<TResult> GetInfoAsync<TParam, TResult>(TParam obj, string spName)
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();
        return await connection.QueryFirstOrDefaultAsync<TResult>(spName, obj, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<TResult>> GetInfoListAsync<TParam, TResult>(TParam obj, string spName)
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();
        return await connection.QueryAsync<TResult>(spName, obj, commandType: CommandType.StoredProcedure);
    }
}
