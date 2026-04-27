using AirlineApp.Models;
using AirlineApp.Repositories;

namespace AirlineApp.Services;

public class AirplaneService(
    IAirplaneRepository airplanes,
    IManufacturerRepository manufacturers,
    IAirlineRepository airlines) : IAirplaneService
{
    public Task<IEnumerable<Airplane>> GetAllAsync() => airplanes.GetAllAsync();

    public Task<Airplane?> GetByIdAsync(int id) => airplanes.GetByIdAsync(id);

    public async Task<(bool Success, string? Error)> CreateAsync(Airplane airplane)
    {
        if (await airplanes.ExistsRegistrationAsync(airplane.RegistrationNumber))
            return (false, $"Літак з реєстрацією '{airplane.RegistrationNumber}' вже існує");

        if (await manufacturers.GetByIdAsync(airplane.ManufacturerId) is null)
            return (false, "Виробника не знайдено");

        if (await airlines.GetByIdAsync(airplane.AirlineId) is null)
            return (false, "Авіакомпанію не знайдено");

        await airplanes.CreateAsync(airplane);
        return (true, null);
    }

    public async Task<(bool Success, string? Error)> UpdateAsync(Airplane airplane)
    {
        if (await airplanes.ExistsRegistrationAsync(airplane.RegistrationNumber, airplane.Id))
            return (false, $"Літак з реєстрацією '{airplane.RegistrationNumber}' вже існує");

        if (await manufacturers.GetByIdAsync(airplane.ManufacturerId) is null)
            return (false, "Виробника не знайдено");

        if (await airlines.GetByIdAsync(airplane.AirlineId) is null)
            return (false, "Авіакомпанію не знайдено");

        await airplanes.UpdateAsync(airplane);
        return (true, null);
    }

    public Task DeleteAsync(int id) => airplanes.DeleteAsync(id);
}
