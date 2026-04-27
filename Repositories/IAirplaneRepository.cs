using AirlineApp.Models;

namespace AirlineApp.Repositories;

public interface IAirplaneRepository
{
    Task<IEnumerable<Airplane>> GetAllAsync();
    Task<Airplane?> GetByIdAsync(int id);
    Task<int> CreateAsync(Airplane airplane);
    Task UpdateAsync(Airplane airplane);
    Task DeleteAsync(int id);
    Task<bool> ExistsRegistrationAsync(string registration, int excludeId = 0);
}
