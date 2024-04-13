using TreeCraftingVoyager.Server.Models.Dto.Category;
using TreeCraftingVoyager.Server.Models.Dto.Shared.EntityDto;

namespace TreeCraftingVoyager.Server.Models.Dto.Product
{
    public class ProductDto: EntityDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime ExpirationDate { get; set; }

        public virtual CategoryDto Category { get; set; }
    }
}
