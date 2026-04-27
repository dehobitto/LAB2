using Microsoft.AspNetCore.Mvc;

namespace AirlineApp.Controllers;

public class ErrorController : Controller
{
    [Route("/error")]
    public IActionResult Index() => View();
}
