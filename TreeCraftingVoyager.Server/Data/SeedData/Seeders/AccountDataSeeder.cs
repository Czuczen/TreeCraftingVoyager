using Microsoft.AspNetCore.Identity;
using TreeCraftingVoyager.Server.Models.Management;

namespace TreeCraftingVoyager.Server.Data.SeedData.Seeders
{
    public class AccountDataSeeder
    {
        public async Task SeedUsersAsync(UserManager<Account> userManager)
        {
            // Example: creating an admin user
            var adminEmail = "admin@treecraftingvoyager.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var newAdminUser = new Account { UserName = adminEmail, Email = adminEmail };
                var result = await userManager.CreateAsync(newAdminUser, "AdminPassword123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdminUser, "Admin");
                }
            }
        }
    }
}
