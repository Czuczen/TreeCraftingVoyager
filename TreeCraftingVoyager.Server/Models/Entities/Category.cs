using System.ComponentModel.DataAnnotations;
using TreeCraftingVoyager.Server.Models.Entities.Shared;

namespace TreeCraftingVoyager.Server.Models.Entities;

public class Category : TreeNode<Category>
{
    [StringLength(250)]
    public string Description { get; set; }

    [StringLength(250)]
    public string ImageURL { get; set; }
}