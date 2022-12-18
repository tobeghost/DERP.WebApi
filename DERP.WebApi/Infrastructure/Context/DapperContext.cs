using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using DERP.WebApi.Domain.Entities;
using MySql.Data.MySqlClient;

namespace DERP.WebApi.Infrastructure.Context;

public class DapperContext : IDisposable
{
    private readonly IDbConnection _connection;
        
    public DapperContext(ErpConfiguration erpConfiguration)
    {
        _connection = erpConfiguration.Type switch
        {
            "MSSQL" => new SqlConnection(erpConfiguration.ConnectionString),
            "MYSQL" => new MySqlConnection(erpConfiguration.ConnectionString),
            _ => throw new Exception($"Not defined connection string for {erpConfiguration.Type}")
        };
        
        _connection.Open();
    }
    
    public async Task<IEnumerable<dynamic>> Query(string sql, object param = null)
    {
        return await _connection.QueryAsync(sql);
    }
    
    public async Task<IEnumerable<T>> Query<T>(string sql, object param = null)
    {
        return await _connection.QueryAsync<T>(sql, param);
    }

    public async Task<bool> Execute(string sql)
    {
        var rowAffect = await _connection.ExecuteAsync(sql);
        return rowAffect > 0;
    }
    
    public void Dispose()
    {
        _connection?.Close();

        GC.SuppressFinalize(this);
    }
}