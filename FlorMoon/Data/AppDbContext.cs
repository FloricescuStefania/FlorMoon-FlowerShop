using FlorMoon.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlorMoon.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
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
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // 1. Creare roluri
            string[] roleNames = { "Administrator", "User" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // 2. Creare Admin implicit
            string adminEmail = "admin@flormoon.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var newAdmin = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var createAdmin = await userManager.CreateAsync(newAdmin, "Admin123!");
                if (createAdmin.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdmin, "Administrator");
                }
            }
        }
    }
}