using Microsoft.AspNetCore.Mvc;
using CUT_Burger.Data;

namespace CUT_Burger.Controllers;

public class UserController : Controller
{
    private readonly SqLiteDbContext _context;

    public UserController(SqLiteDbContext context)
    {
        _context = context;
    }

    public IActionResult UserDashboard()
    {
        // Check if the "Username" cookie exists, and if it doesn't, default to "Guest"
        if (Request.Cookies.TryGetValue("Username", out var username))
        {
            ViewData["Username"] = username;
        }
        else
        {
            ViewData["Username"] = "Guest"; // Default to "Guest" if cookie is not present
        }

        // Fetch the items from the database
        var items = _context.Burgers.ToList();

        // Pass the items to the view
        return View(items);
    }
}