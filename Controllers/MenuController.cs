using CUT_Burger.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CUT_Burger.Controllers;

public class MenuController : Controller
{
    private readonly BurgerRepository _burgerRepository;

    public MenuController(BurgerRepository burgerRepository)
    {
        _burgerRepository = burgerRepository;
    }

    public IActionResult Index()
    {
        var burgers = _burgerRepository.GetAllBurgers(); // Fetch burgers from the database
        return View(burgers);
    }
}