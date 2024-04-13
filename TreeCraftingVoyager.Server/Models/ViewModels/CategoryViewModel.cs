using TreeCraftingVoyager.Server.Models.Entities;

namespace TreeCraftingVoyager.Server.Models.ViewModels
{
    public class CategoryViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageURL { get; set; }

        public int DisplayOrder { get; set; }

        public virtual ICollection<Category> Childrens { get; set; } = new List<Category>();
    }
}
