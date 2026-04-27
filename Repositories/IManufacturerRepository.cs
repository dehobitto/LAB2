using AirlineApp.Models;

namespace AirlineApp.Repositories;

public interface IManufacturerRepository
{
    Task<IEnumerable<Manufacturer>> GetAllAsync();
    Task<Manufacturer?> GetByIdAsync(int id);
}
