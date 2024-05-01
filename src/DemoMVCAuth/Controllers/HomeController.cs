using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DemoMVCAuth.Models;
using Microsoft.AspNetCore.Authorization;

namespace DemoMVCAuth.Controllers;

public class HomeController : Controller
{

    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(/*[FromForm] Student student*/)
    {
        /*
        if (Request.Method == "POST")
        {
            if (string.IsNullOrEmpty(student.FirstName))
            {
                ModelState.AddModelError("FirstName", "First Name is required.");
            }
            if (string.IsNullOrEmpty(student.LastName))
            {
                ModelState.AddModelError("LastName", "Last Name is required.");
            }
            if (ModelState.IsValid)
            {
                // student.UserID = User.Identity.Name;
                students.Add(student);
            }
        }
        */
        return View(/*students.Where(student => student.UserID == User.Identity.Name).ToList()*/);

    }
    //[Authorize]
    public IActionResult Privacy()
    {
        //if (!User.Identity.IsAuthenticated)
        //{
        //return RedirectToAction(nameof(Index));
        //}
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
