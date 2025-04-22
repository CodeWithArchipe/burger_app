using CUT_Burger.Data;
using CUT_Burger.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CUT_Burger.Controllers
{
    public class AccountController : Controller
    {
        private readonly SqLiteDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly ILogger<AccountController> _logger;

        // Constructor with logger injection
        public AccountController(SqLiteDbContext context, ILogger<AccountController> logger)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
            _logger = logger;
        }

        // GET: Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

       // POST: Login
[HttpPost]
public async Task<IActionResult> Login(LoginViewModel model)
{
    if (!ModelState.IsValid)
    {
        // Log errors
        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        {
            _logger.LogError("Login Error: {Error}", error.ErrorMessage);
        }
        return View(model);
    }

    // Find the user by username
    var user = _context.Users.SingleOrDefault(u => u.Username == model.Username);

    if (user != null)
    {
        // Verify the password hash
        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, model.Password);

        if (result == PasswordVerificationResult.Success)
        {
            // Create the user's claims (including UserId)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) // Adding UserId as a claim
            };

            // Create the claims identity
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Sign in the user
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1) // Cookie expiration time
            });

            // Log successful login attempt
            _logger.LogInformation("User {Username} logged in successfully.", user.Username);

            // Redirect based on user's role (Admin or User)
            if (user.IsAdmin)
            {
                return RedirectToAction("AdminDashboard", "Admin");
            }
            else
            {
                return RedirectToAction("UserDashboard", "User");
            }
        }
        else
        {
            // Log failed password verification
            _logger.LogWarning("Invalid password attempt for user {Username}.", model.Username);
        }
    }
    else
    {
        // Log that the user does not exist
        _logger.LogWarning("Invalid login attempt: user {Username} does not exist.", model.Username);
    }

    // If we get here, something went wrong: add a model error for invalid login
    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
    return View(model);
}

        // Logout action
        public IActionResult Logout()
        {
            Response.Cookies.Delete("Username");
            return RedirectToAction("Login");
        }

        // GET: Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Check if the username or email already exists
            if (_context.Users.Any(u => u.Username == model.Username))
            {
                ModelState.AddModelError("Username", "Username is already taken.");
                return View(model);
            }

            if (_context.Users.Any(u => u.Email == model.Email))
            {
                ModelState.AddModelError("Email", "Email is already taken.");
                return View(model);
            }

            var user = new User
            {
                Username = model.Username,
                Email = model.Email, // Ensure Email is populated
                City = model.City,
                State = model.State,
                Country = model.Country,
                Address = new UserAddress // Ensure UserAddress is properly instantiated
                {
                    Address = model.Address,
                    City = model.City,
                    State = model.State,
                    Country = model.Country
                }
            };

            // Hash the password after the user is created
            user.Password = _passwordHasher.HashPassword(user, model.Password);

            // Add the user to the database
            _context.Users.Add(user);
    
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving the user.");
                ModelState.AddModelError("", "An error occurred while saving the user. Please try again.");
                return View(model);
            }

            // Log successful registration
            _logger.LogInformation("User {Username} registered successfully.", user.Username);

            return RedirectToAction("Login");
        }

    }

    
}
