using Microsoft.AspNetCore.Mvc;

namespace AirlineApp.Controllers;

public class HomeController : Controller
{
    [HttpGet("/")]
    public IActionResult Index() => View();
}
