using System.Runtime.Loader;
using Microsoft.EntityFrameworkCore;

namespace Coffe_site.Models
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

     
        
        public DbSet<User> Users { get; set; }

        public DbSet<Address> Address { get; set; }


        public DbSet<Item> Items { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<Order> Orders { get; set; }
    }
}
