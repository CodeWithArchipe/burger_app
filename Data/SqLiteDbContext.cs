using Microsoft.EntityFrameworkCore;
using CUT_Burger.Models;

namespace CUT_Burger.Data
{
    public class SqLiteDbContext : DbContext
    {
        public SqLiteDbContext(DbContextOptions<SqLiteDbContext> options) : base(options)
        {
        }

        // DbSet for users
        public DbSet<User> Users { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }

        // DbSet for items (burgers)
        public DbSet<Burger> Burgers { get; set; } 

        // DbSet for orders
        public DbSet<Order> Orders { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User entity
            modelBuilder.Entity<User>()
                .ToTable("Users")
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Address)
                .WithOne(a => a.User)
                .HasForeignKey<UserAddress>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Optionally specify delete behavior

            // Configure UserAddress entity
            modelBuilder.Entity<UserAddress>()
                .ToTable("UserAddresses")
                .HasKey(a => a.UserId);

            // Configure Burger entity
            modelBuilder.Entity<Burger>()
                .ToTable("Burgers")
                .HasKey(b => b.Id);

            // Configure Order entity
            modelBuilder.Entity<Order>()
                .ToTable("Orders")
                .HasKey(o => o.OrderId);

            // Configure relationships for Order and User
            modelBuilder.Entity<Order>()
                .HasOne<User>(o => o.User)
                .WithMany(u => u.Orders) // Assuming User has a collection of Orders
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Specify delete behavior if necessary
        }
    }
}