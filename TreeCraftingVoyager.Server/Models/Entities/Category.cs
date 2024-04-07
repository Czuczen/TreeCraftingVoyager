using System.ComponentModel.DataAnnotations;
using TreeCraftingVoyager.Server.Models.Entities.Shared;

namespace TreeCraftingVoyager.Server.Models.Entities
{
    public class Category : TreeNode<Category>
    {
        public string Description { get; set; }

        [Url]
        public string ImageURL { get; set; }

        public string SEOKeywords { get; set; }

        public bool IsActive { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
