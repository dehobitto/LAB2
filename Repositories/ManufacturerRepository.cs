using AirlineApp.Data;
using AirlineApp.Models;
using Dapper;

namespace AirlineApp.Repositories;

public class ManufacturerRepository(Database db) : IManufacturerRepository
{
    public async Task<IEnumerable<Manufacturer>> GetAllAsync()
    {
        using var conn = db.CreateConnection();
        return await conn.QueryAsync<Manufacturer>("SELECT * FROM Manufacturers ORDER BY Name");
    }

    public async Task<Manufacturer?> GetByIdAsync(int id)
    {
        using var conn = db.CreateConnection();
        return await conn.QueryFirstOrDefaultAsync<Manufacturer>(
            "SELECT * FROM Manufacturers WHERE Id = @id", new { id });
    }
}
