using TreeCraftingVoyager.Server.Models.Dto.Shared.EntityDto;

namespace TreeCraftingVoyager.Server.Models.Dto.Category
{
    public class UpdateCategoryDto : EntityDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        //[Url]
        public string ImageURL { get; set; }

        public int DisplayOrder { get; set; }

        public long? ParentId { get; set; }
    }
}
