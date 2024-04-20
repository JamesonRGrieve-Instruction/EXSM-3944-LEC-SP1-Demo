using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DemoMVC.Models;
using System.Text.Json;
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
        // The initial request to a page will always be a GET, so if you want to only validate when data is submitted, you can do a POST check.
        if (Request.Method == "POST")
        {
            // ModelState is automatically included in ViewData, so it's a little more semantic than using ViewBag.
            // Automated validation can be done in multiple ways, which we will look at later.
            if (string.IsNullOrEmpty(model.FirstName))
            {
                ModelState.AddModelError(nameof(model.FirstName), "First Name is required.");
            }
            if (string.IsNullOrEmpty(model.LastName))
            {
                ModelState.AddModelError(nameof(model.LastName), "Last Name is required.");
            }
            if (string.IsNullOrEmpty(model.Email))
            {
                ModelState.AddModelError(nameof(model.Email), "Email is required.");
            }
            if (ModelState.IsValid)
            {
                // TempData is required to pass data between redirects, as ViewData and ViewBag do not persist.
                // This is encoded in a forwarded request, and therefore must be a primitive type (objects must be JSONified).
                TempData["ValidatedModel"] = JsonSerializer.Serialize(model);
                return RedirectToAction(nameof(SampleOut));
            }
        }
        // Passing an argument to View() will inject the value into the @model (Model) of the view if it is set.
        return View(model);
    }
    // Since this action is just a renderer for already validated data, we can just deserialize the JSON directly into View().
    public IActionResult SampleOut() => View(JsonSerializer.Deserialize<SampleFormModel>(TempData["ValidatedModel"] as string));
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
