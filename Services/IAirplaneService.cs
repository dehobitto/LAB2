using AirlineApp.Models;

namespace AirlineApp.Services;

public interface IAirplaneService
{
    Task<IEnumerable<Airplane>> GetAllAsync();
    Task<Airplane?> GetByIdAsync(int id);
    Task<(bool Success, string? Error)> CreateAsync(Airplane airplane);
    Task<(bool Success, string? Error)> UpdateAsync(Airplane airplane);
    Task DeleteAsync(int id);
}
