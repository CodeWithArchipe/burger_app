using CUT_Burger.Data; // Adjust the namespace according to your project
using System.Collections.Generic;
using System.Linq;
using CUT_Burger.Models;

namespace CUT_Burger.Repositories
{
    public class BurgerRepository
    {
        private readonly SqLiteDbContext _context;

        public BurgerRepository(SqLiteDbContext context)
        {
            _context = context;
        }

        public List<Burger> GetAllBurgers()
        {
            return _context.Burgers.ToList(); // Fetches all burgers from the database
        }
    }
}