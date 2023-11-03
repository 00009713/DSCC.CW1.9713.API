using DSCC.CW1._9713.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DSCC.CW1._9713.API.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define the primary key for the Customer entity
            modelBuilder.Entity<Customer>()
                .HasKey(c => c.Id);

            // Define the primary key for the Order entity
            modelBuilder.Entity<Order>()
                .HasKey(o => o.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
