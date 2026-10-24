using FlorMoon.Models;
using Microsoft.EntityFrameworkCore;

namespace FlorMoon.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories{ get; set; }
        public DbSet<Product> Products{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category{ CategoryID = 1, CategoryName = "roses" },
                new Category{ CategoryID = 2, CategoryName = "tulips" },
                new Category{ CategoryID = 3, CategoryName = "peonies" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product{ ProductID = 1, ProductName = "Pink and White Roses Bouquet", Price = 50, CategoryID = 1, ImagePath = "pinkWhiteRoses.png", ProductDescription = "A beautiful mix of pink and white roses." },
                new Product{ ProductID = 2, ProductName = "White Roses Bouquet", Price = 70, CategoryID = 1, ImagePath = "whiteRoses.png", ProductDescription = "Elegant all-white rose bouquet." },
                new Product{ ProductID = 3, ProductName = "Pink Peony Bouquet", Price = 60, CategoryID = 3, ImagePath = "pinkPeony.png", ProductDescription = "Sweet pink peonies." },
                new Product{ ProductID = 4, ProductName = "Pink Tulips Bouquet", Price = 55, CategoryID = 2, ImagePath = "pinkTulips.png", ProductDescription = "Fresh spring tulips." }
            );
        }
    }
}