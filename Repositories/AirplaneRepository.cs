using AirlineApp.Data;
using AirlineApp.Models;
using Dapper;

namespace AirlineApp.Repositories;

public class AirplaneRepository(
    Database db,
    IManufacturerRepository manufacturers,
    IAirlineRepository airlines) : IAirplaneRepository
{
    public async Task<IEnumerable<Airplane>> GetAllAsync()
    {
        using var conn = db.CreateConnection();
        return await conn.QueryAsync<Airplane>("SELECT * FROM Airplanes ORDER BY Model");
    }

    public async Task<Airplane?> GetByIdAsync(int id)
    {
        using var conn = db.CreateConnection();
        var airplane = await conn.QueryFirstOrDefaultAsync<Airplane>(
            "SELECT * FROM Airplanes WHERE Id = @id", new { id });

        if (airplane is null) return null;

        airplane.Manufacturer = await manufacturers.GetByIdAsync(airplane.ManufacturerId);
        airplane.Airline      = await airlines.GetByIdAsync(airplane.AirlineId);
        return airplane;
    }

    public async Task<int> CreateAsync(Airplane airplane)
    {
        using var conn = db.CreateConnection();
        return await conn.ExecuteScalarAsync<int>("""
            INSERT INTO Airplanes (Model, RegistrationNumber, Capacity, YearManufactured, ManufacturerId, AirlineId)
            VALUES (@Model, @RegistrationNumber, @Capacity, @YearManufactured, @ManufacturerId, @AirlineId);
            SELECT last_insert_rowid();
            """, airplane);
    }

    public async Task UpdateAsync(Airplane airplane)
    {
        using var conn = db.CreateConnection();
        await conn.ExecuteAsync("""
            UPDATE Airplanes
            SET Model              = @Model,
                RegistrationNumber = @RegistrationNumber,
                Capacity           = @Capacity,
                YearManufactured   = @YearManufactured,
                ManufacturerId     = @ManufacturerId,
                AirlineId          = @AirlineId
            WHERE Id = @Id
            """, airplane);
    }

    public async Task DeleteAsync(int id)
    {
        using var conn = db.CreateConnection();
        await conn.ExecuteAsync("DELETE FROM Airplanes WHERE Id = @id", new { id });
    }

    public async Task<bool> ExistsRegistrationAsync(string registration, int excludeId = 0)
    {
        using var conn = db.CreateConnection();
        var count = await conn.ExecuteScalarAsync<int>(
            "SELECT COUNT(*) FROM Airplanes WHERE RegistrationNumber = @registration AND Id != @excludeId",
            new { registration, excludeId });
        return count > 0;
    }
}
