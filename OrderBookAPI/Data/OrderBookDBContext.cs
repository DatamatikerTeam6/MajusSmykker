using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrderBookAPI.Models;

namespace OrderBookAPI.Data
{
    public class OrderBookDBContext : IdentityDbContext<ApplicationUser>
    {            
        // Database context
        public OrderBookDBContext(DbContextOptions<OrderBookDBContext> options) : base(options) 
        {         
        }

        // Database tables
        public DbSet<Order> Orders { get; set; }
        public DbSet<Order> Customers { get; set; }

        // Override database tablenames
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Customer>().ToTable("Customer");
        }
    }
}
