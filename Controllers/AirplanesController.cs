using AirlineApp.Models;
using AirlineApp.Repositories;
using AirlineApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirlineApp.Controllers;

public class AirplanesController(
    IAirplaneService service,
    IManufacturerRepository manufacturers,
    IAirlineRepository airlines) : Controller
{
    [HttpGet("/airplanes")]
    public async Task<IActionResult> Index()
    {
        var list = await service.GetAllAsync();
        return View(list);
    }

    [HttpGet("/airplanes/create")]
    public async Task<IActionResult> Create()
    {
        return View("Form", await BuildViewModel(new Airplane(), "create"));
    }

    [HttpPost("/airplanes/create")]
    public async Task<IActionResult> CreatePost(Airplane airplane)
    {
        if (!ModelState.IsValid)
            return View("Form", await BuildViewModel(airplane, "create"));

        var (success, error) = await service.CreateAsync(airplane);
        if (!success)
        {
            ModelState.AddModelError("", error!);
            return View("Form", await BuildViewModel(airplane, "create"));
        }

        return Redirect("/airplanes");
    }

    [HttpGet("/airplanes/{id:int}")]
    public async Task<IActionResult> Details(int id)
    {
        var airplane = await service.GetByIdAsync(id);
        if (airplane is null) return NotFound();
        return View("Form", await BuildViewModel(airplane, "view"));
    }

    [HttpGet("/airplanes/{id:int}/edit")]
    public async Task<IActionResult> Edit(int id)
    {
        var airplane = await service.GetByIdAsync(id);
        if (airplane is null) return NotFound();
        return View("Form", await BuildViewModel(airplane, "edit"));
    }

    [HttpPost("/airplanes/{id:int}/edit")]
    public async Task<IActionResult> EditPost(int id, Airplane airplane)
    {
        airplane.Id = id;

        if (!ModelState.IsValid)
            return View("Form", await BuildViewModel(airplane, "edit"));

        var (success, error) = await service.UpdateAsync(airplane);
        if (!success)
        {
            ModelState.AddModelError("", error!);
            return View("Form", await BuildViewModel(airplane, "edit"));
        }

        return Redirect("/airplanes");
    }

    [HttpPost("/airplanes/{id:int}/delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeleteAsync(id);
        return Redirect("/airplanes");
    }

    private async Task<AirplaneFormViewModel> BuildViewModel(Airplane airplane, string mode) =>
        new()
        {
            Airplane      = airplane,
            Mode          = mode,
            Manufacturers = (await manufacturers.GetAllAsync()).ToList(),
            Airlines      = (await airlines.GetAllAsync()).ToList(),
        };
}
