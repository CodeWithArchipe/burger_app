using CUT_Burger.Data;
using CUT_Burger.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CUT_Burger.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SqLiteDbContext _context;
    private readonly IDbInitializer _seedDatabase;

    public HomeController(ILogger<HomeController> logger, SqLiteDbContext context,
        IDbInitializer seedDatabase)
    {
        _logger = logger;
        _context = context;
        _seedDatabase = seedDatabase;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult SeedDatabase()
    {
        _seedDatabase.Initialize(_context);
        ViewBag.SeedDbFeedback = "Database created and User Table populated with Data. Check Database folder.";
        return View("SeedDatabase");
    }

    [HttpPost]
    public IActionResult CheckAdminStatus(bool isAdmin)
    {
        return RedirectToAction("Login", "Account"); // Redirect to login 
    }
    
    [Authorize]
    public IActionResult Order()
    {
        // Your order logic here
        return RedirectToAction("login", "Account");
    }

}