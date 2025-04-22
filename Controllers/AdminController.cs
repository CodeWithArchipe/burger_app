using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CUT_Burger.Data;
using CUT_Burger.Models;

namespace CUT_Burger.Controllers
{
    public class AdminController : Controller
    {
        private readonly SqLiteDbContext _context;

        public AdminController(SqLiteDbContext context)
        {
            _context = context;
        }

        // Action for the admin dashboard
        public async Task<IActionResult> AdminDashboard()
        {
            var users = await _context.Users.Include(u => u.Address).ToListAsync();
            return View(users);
        }

        // Action to view all users
        public async Task<IActionResult> ViewAllUsers()
        {
            var users = await _context.Users.Include(u => u.Address).ToListAsync();
            return View(users);
        }

        // Action for managing users
        public async Task<IActionResult> ManageUsers()
        {
            var users = await _context.Users.Include(u => u.Address).ToListAsync();
            return View(users);
        }

        // Action for editing a user
        public async Task<IActionResult> EditUser(int id)
        {
            var user = await _context.Users.Include(u => u.Address)
                .SingleOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Action for saving the edited user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Update(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageUsers));
            }

            // If the model state is invalid, return to the edit view
            return View(user);
        }

        // Action for deleting a user
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.Include(u => u.Address)
                .SingleOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Action for confirming user deletion
        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUserConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(ManageUsers));
        }
        public async Task<IActionResult> ViewOrders()
        {
            var orders = await _context.Orders.Include(o => o.User).ToListAsync(); // Include any related entities as needed
            return View(orders);
        }
        

    }
}
