using Clothes_Web_API_App.Models;
using Microsoft.EntityFrameworkCore;

namespace Clothes_Web_API_App.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<OrderItem>().HasKey(e => new { e.ClothItemId, e.OrderId });
            modelBuilder.Entity<Review>().HasKey(e => new { e.UserId, e.ClothId });
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Cloth> Cloths { get; set; }
        public DbSet<ClothItem> ClothItems { get; set; }
        public DbSet<ClothImage> ClothImages { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<UserNumber> UserNumbers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
