using AirlineApp.Models;

namespace AirlineApp.Repositories;

public interface IAirlineRepository
{
    Task<IEnumerable<Airline>> GetAllAsync();
    Task<Airline?> GetByIdAsync(int id);
}
