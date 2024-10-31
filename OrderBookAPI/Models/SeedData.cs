using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrderBookAPI.Data;

namespace OrderBookAPI.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new OrderBookDBContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<OrderBookDBContext>>()))
            {
                context.Database.EnsureCreated();

                var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                // Check if the DB has been seeded
                if (context.Users.Any())
                {
                    return;   // DB has been seeded
                }

                // Add roles and users
                string[] roles = new string[] { "Admin", "User" };

                foreach (var role in roles)
                {
                    if (!roleManager.RoleExistsAsync(role).Result)
                    {
                        roleManager.CreateAsync(new IdentityRole(role)).Wait();
                    }
                }

                // Seed admin user
                var adminUser = new ApplicationUser
                {
                    UserName = "admin@majus.dk",
                    Email = "admin@majus.dk",
                    EmailConfirmed = true
                };


                if (userManager.FindByEmailAsync(adminUser.Email).Result == null)
                {
                    var result = userManager.CreateAsync(adminUser, "Admin123!").Result;
                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(adminUser, "Admin").Wait();
                    }
                }

                // Seed regular user
                var regularUser = new ApplicationUser
                {
                    UserName = "regular@majus.dk",
                    Email = "regular@majus.dk",
                    EmailConfirmed = true
                };

                if (userManager.FindByEmailAsync(regularUser.Email).Result == null)
                {
                    var result = userManager.CreateAsync(regularUser, "Regular123!").Result;
                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(regularUser, "User").Wait();
                    }
                };
                context.SaveChanges();
            }
        }
    }
}
