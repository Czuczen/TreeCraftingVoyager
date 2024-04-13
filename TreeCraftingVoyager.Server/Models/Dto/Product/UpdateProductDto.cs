using TreeCraftingVoyager.Server.Models.Dto.Shared.EntityDto;

namespace TreeCraftingVoyager.Server.Models.Dto.Product
{
    public class UpdateProductDto : EntityDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
