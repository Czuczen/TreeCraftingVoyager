using Microsoft.AspNetCore.Identity;
using TreeCraftingVoyager.Server.Models.Entities.Shared.EntityBase;

namespace TreeCraftingVoyager.Server.Models.Management;

public class Role : IdentityRole, IEntityBase<string>
{
}