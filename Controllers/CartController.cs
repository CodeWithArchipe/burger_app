using Microsoft.AspNetCore.Mvc;
using CUT_Burger.Models;
using CUT_Burger.Data;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CUT_Burger.Controllers
{
    public class CartController : Controller
    {
        private const string SessionKeyCart = "Cart";
        private readonly SqLiteDbContext _context;
        private readonly ILogger<CartController> _logger;

        // Inject the DbContext through the constructor
        public CartController(SqLiteDbContext context, ILogger<CartController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Display the cart
        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        // Add an item to the cart
        public IActionResult AddToCart(int id, string name, decimal price)
        {
            var cart = GetCart();

            // Check if the item already exists in the cart
            var cartItem = cart.Items.FirstOrDefault(i => i.Id == id);

            if (cartItem != null)
            {
                // Increment the quantity if the item already exists
                cartItem.Quantity++;
            }
            else
            {
                // Add a new item to the cart
                cart.Items.Add(new BurgerCartItem
                {
                    Id = id,
                    Name = name,
                    Price = price,
                    Quantity = 1
                });
            }

            // Save the updated cart
            SaveCart(cart);
            return RedirectToAction("Index");
        }

        // Remove an item from the cart
        public IActionResult RemoveFromCart(int id)
        {
            var cart = GetCart();
            var cartItem = cart.Items.FirstOrDefault(i => i.Id == id);

            if (cartItem != null)
            {
                cart.Items.Remove(cartItem);
                SaveCart(cart);
            }

            return RedirectToAction("Index");
        }

        // Display the checkout form
        public IActionResult Checkout()
        {
            var cart = GetCart();
            if (cart.Items.Count == 0)
            {
                return RedirectToAction("Index");
            }

            // Retrieve the user ID from the claims
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = string.IsNullOrEmpty(userIdString) ? 0 : int.Parse(userIdString);

            // Pass the cart and user ID to the view
            var checkoutViewModel = new CheckoutViewModel
            {
                Items = cart.Items,
                PaymentMethod = "Cash on Delivery",
                UserId = userId // Set the UserId
            };

            return View(checkoutViewModel);
        }

        // Handle the submission of the order
        [HttpPost]
        [Authorize]
        public IActionResult SubmitOrder(CheckoutViewModel model)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = string.IsNullOrEmpty(userIdString) ? 0 : int.Parse(userIdString);
            // var userId = model.UserId;

            // Check if the user exists before creating the order
            var userExists = _context.Users.Any(u => u.Id == userId);
            if (!userExists)
            {
                ModelState.AddModelError("", "User does not exist.");
                return View("Checkout", model);
            }

            // Validate the model state
            if (!ModelState.IsValid)
            {
                return View("Checkout", model); // Return to checkout with validation errors
            }

            var cart = GetCart();

            // Create the order object
            var order = new Order
            {
                OrderNumber = Guid.NewGuid().ToString(),
                PaymentMethod = model.PaymentMethod,
                TotalAmount = cart.Items.Sum(item => item.Price * item.Quantity),
                EstimatedDeliveryTime = "30 minutes",
                OrderStatus = "Pending",
                UserId = userId,  // Set the actual user ID from the claims
                UserName = model.Name,
                UserAddress = model.Address,
                UserCity = model.City,
                UserState = model.State,
                UserZip = model.Zip
            };

            // Save the order to the database
            _context.Orders.Add(order);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Log the exception and show a friendly error message
                _logger.LogError(ex, "An error occurred while saving the order.");
                ModelState.AddModelError("", "An error occurred while processing your order. Please try again.");
                return View("Checkout", model);
            }

            // Clear the cart after the order is submitted
            SaveCart(new Cart());

            // Redirect to the order confirmation page
            return RedirectToAction("OrderConfirmation", new { orderNumber = order.OrderNumber });
        }


        // Display the order confirmation page
        public IActionResult OrderConfirmation(string orderNumber)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderNumber == orderNumber);
            if (order == null)
            {
                return NotFound(); 
            }
            return View(order); 
        }

        // Retrieve the cart from the session
        private Cart GetCart()
        {
            var sessionCart = HttpContext.Session.GetString(SessionKeyCart);
            return sessionCart == null ? new Cart() : JsonConvert.DeserializeObject<Cart>(sessionCart);
        }

        // Save the cart to the session
        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetString(SessionKeyCart, JsonConvert.SerializeObject(cart));
        }
    }
}
