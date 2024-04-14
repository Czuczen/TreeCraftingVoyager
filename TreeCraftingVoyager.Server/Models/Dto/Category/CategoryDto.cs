using TreeCraftingVoyager.Server.Models.Dto.Shared;

namespace TreeCraftingVoyager.Server.Models.Dto.Category;

public class CategoryDto : TreeNodeDto<CategoryDto>
{
    public string Description { get; set; }

    public string ImageURL { get; set; }
}