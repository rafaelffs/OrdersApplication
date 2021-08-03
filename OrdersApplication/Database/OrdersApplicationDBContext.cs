using Microsoft.EntityFrameworkCore;
using OrdersApplication.Models;

namespace OrdersApplication.Database
{
    public class OrdersApplicationDBContext : DbContext
    {
        public OrdersApplicationDBContext(DbContextOptions options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Location> Locations { get; set; }
    }
}
