using System.Data;
using Microsoft.Data.Sqlite;

namespace AirlineApp.Data;

public class Database
{
    private readonly string _connectionString;

    public Database(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default")
            ?? "Data Source=airline.db";
    }

    public IDbConnection CreateConnection() => new SqliteConnection(_connectionString);
}
