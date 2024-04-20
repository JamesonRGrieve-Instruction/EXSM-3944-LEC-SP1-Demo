using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DemoMVC.Models;

namespace DemoMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    // According to the pathing schema defined in Program.cs, the controller will map views from the Views folder.
    // By default it will take the name of the controller as the folder, and the name of the function as the .cshtml file.
    public IActionResult Index()
    {
        // Returning View() will inject the view into the layout and send it to the client.
        return View();
    }
    // Rather than performing a decision to determine the method, you may find it easier to move the initial load into a dedicated GET handler.
    /*
    [HttpGet]
    public IActionResult Sample()
    {
        ViewBag.Errors = new List<string>();
        return View();
    }
    [HttpPost]
    */

    public IActionResult Sample([FromForm] SampleFormModel model)
    {
        ViewBag.Errors = new List<string>();
        if (Request.Method == "POST")
        {
            if (string.IsNullOrEmpty(model.FirstName))
            {
                ModelState.AddModelError(nameof(model.FirstName), "First Name is required.");
            }
            if (string.IsNullOrEmpty(model.LastName))
            {
                ModelState.AddModelError(nameof(model.LastName), "Last Name is required.");
            }
        }
        return View(model ?? new SampleFormModel());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
