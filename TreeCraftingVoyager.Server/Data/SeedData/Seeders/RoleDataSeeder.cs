using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
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

    public async Task SeedRolesAsync()
    {
        //if (!await _roleManager.RoleExistsAsync("Admin"))
        //{
        //    var adminRole = new Role { Name = "Admin" };
        //    await _roleManager.CreateAsync(adminRole);
        //    await _roleManager.AddClaimAsync(adminRole, new Claim("permission", "edit_products"));
        //    await _roleManager.AddClaimAsync(adminRole, new Claim("permission", "view_orders"));
        //}

        //if (!await _roleManager.RoleExistsAsync("Vendor"))
        //{
        //    var vendorRole = new Role { Name = "Vendor" };
        //    await _roleManager.CreateAsync(vendorRole);
        //    await _roleManager.AddClaimAsync(vendorRole, new Claim("permission", "manage_inventory"));
        //}

        //if (!await _roleManager.RoleExistsAsync("Customer"))
        //{
        //    var customerRole = new Role { Name = "Customer" };
        //    await _roleManager.CreateAsync(customerRole);
        //}
    }
}
