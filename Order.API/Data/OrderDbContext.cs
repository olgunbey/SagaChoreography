using Microsoft.EntityFrameworkCore;

namespace Order.API.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> dbContextOptions) : base(dbContextOptions) { }
        public DbSet<Entities.Order> Order { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Order>().Ignore(u => u.OrderItems);
        }
    }

}
