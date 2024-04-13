using System.ComponentModel.DataAnnotations;
using TreeCraftingVoyager.Server.Models.Entities.Shared;

namespace TreeCraftingVoyager.Server.Models.Entities
{
    public class Category : TreeNode<Category>
    {
        public string Description { get; set; }

        public string ImageURL { get; set; }
    }
}
