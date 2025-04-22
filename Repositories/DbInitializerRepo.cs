using CUT_Burger.Data;
using CUT_Burger.Models;
using Microsoft.AspNetCore.Identity;
using CUT_Burger.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CUT_Burger.Repositories
{
    public class DbInitializerRepo : IDbInitializer
    {
        private readonly PasswordHasher<User> _passwordHasher;

        public DbInitializerRepo()
        {
            _passwordHasher = new PasswordHasher<User>();
        }

        public void Initialize(SqLiteDbContext context)
        {
            // Ensure the database is created
            context.Database.EnsureCreated();

            // Check if there are any users in the database
            if (context.Users.Any())
            {
                return; // DB has been seeded
            }

            // Create the admin user
            var admin = CreateUser("Admin", "admin@example.com", "AdminPassword123!", true);

            // Create the regular user
            var regularUser = CreateUser("RegularUser", "user@example.com", "UserPassword123!", false);

            // Create Mickey with an address
            var mickey = CreateUserWithAddress("Mickey", "mickey.strud@example.com", "Password123!", "111 Tintdy Lane", "Welkom", "Free State", "South Africa");

            // Create Strud with an address
            var strud = CreateUserWithAddress("Strud", "strud.morkel@example.com", "Password123!", "222 Hollard Lane", "Welkom", "Free State", "South Africa");

            // Add users to the context
            context.Users.AddRange(admin, regularUser, mickey, strud);

            // Create burger items
            var items = new List<Burger>
            {
                new Burger
                {
                    Name = "Classic Beef Burger",
                    Price = 50.00m,
                    Description = "A juicy beef patty with lettuce, tomato, and cheese.",
                    Availability = true
                },
                new Burger
                {
                    Name = "Spicy Chicken Burger",
                    Price = 55.00m,
                    Description = "Crispy chicken fillet with spicy mayo, lettuce, and pickles.",
                    Availability = true
                },
                new Burger
                {
                    Name = "Veggie Delight Burger",
                    Price = 45.00m,
                    Description = "A healthy veggie patty with lettuce, tomato, and avocado.",
                    Availability = true
                },
                new Burger
                {
                    Name = "Cheese Lover's Burger",
                    Price = 40.00m,
                    Description = "Loaded with melted cheese for the ultimate cheesy experience.",
                    Availability = true
                },
                new Burger
                {
                    Name = "BBQ Bacon Burger",
                    Price = 40.00m,
                    Description = "A mouthwatering BBQ burger with crispy bacon.",
                    Availability = true
                }
            };

            // Add items to the context
            context.Burgers.AddRange(items);

            // Save changes to the database
            context.SaveChanges();
        }

        // Helper method to create a user
        private User CreateUser(string username, string email, string password, bool isAdmin)
        {
            var user = new User
            {
                Username = username,
                Email = email,
                IsAdmin = isAdmin,
            };
            user.Password = _passwordHasher.HashPassword(user, password); // Pass the user object
            return user;
        }

        // Helper method to create a user with an address
        // Assuming you want to create a user with an address:
        private User CreateUserWithAddress(string username, string email, string password, string address, string city, string state, string country)
        {
            // Create a UserAddress object
            var userAddress = new UserAddress
            {
                Address = address,
                City = city,
                State = state,
                Country = country
            };

            // Create the user and assign the address
            var user = new User
            {
                Username = username,
                Email = email,
                Address = userAddress, // Correctly assign UserAddress object
                Password = _passwordHasher.HashPassword(new User(), password) // Hash the password with a new User object
            };

            return user;
        }

    }
}
