using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class FlightSearchController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}