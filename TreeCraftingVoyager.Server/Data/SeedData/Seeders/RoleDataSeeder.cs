using Microsoft.AspNetCore.Identity;
using TreeCraftingVoyager.Server.Models.Management;

namespace TreeCraftingVoyager.Server.Data.SeedData.Seeders;

public class RoleDataSeeder
{
    public async Task SeedRolesAsync(RoleManager<Role> roleManager)
    {
            var roles = new[] { "Admin", "User", "Seller" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new Role { Name = role });
                }
            }
        }
}