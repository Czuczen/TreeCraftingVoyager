using System.ComponentModel.DataAnnotations;

namespace TreeCraftingVoyager.Server.Models.Management;

public class RegisterModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
