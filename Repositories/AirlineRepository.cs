using AirlineApp.Data;
using AirlineApp.Models;
using Dapper;

namespace AirlineApp.Repositories;

public class AirlineRepository(Database db) : IAirlineRepository
{
    public async Task<IEnumerable<Airline>> GetAllAsync()
    {
        using var conn = db.CreateConnection();
        return await conn.QueryAsync<Airline>("SELECT * FROM Airlines ORDER BY Name");
    }

    public async Task<Airline?> GetByIdAsync(int id)
    {
        using var conn = db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Airline>(
            "SELECT * FROM Airlines WHERE Id = @id", new { id });
    }
}
