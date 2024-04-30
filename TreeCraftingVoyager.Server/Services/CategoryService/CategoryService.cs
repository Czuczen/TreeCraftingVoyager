using TreeCraftingVoyager.Server.Data.Repositories.Crud;
using TreeCraftingVoyager.Server.Models.Dto.Category;
using TreeCraftingVoyager.Server.Models.Entities;

namespace TreeCraftingVoyager.Server.Services.CategoryService;

public class CategoryService : ICategoryService
{
    private readonly ICrudRepository<Category, CategoryDto, UpdateCategoryDto, CreateCategoryDto> _crudRepository;

    public CategoryService(ICrudRepository<Category, CategoryDto, UpdateCategoryDto, CreateCategoryDto> crudRepository)
    {
            _crudRepository = crudRepository;
    }

    public async Task<CategoryDto> UpdateCategory(UpdateCategoryDto updateDto)
    {
        if (updateDto.Id == updateDto.ParentId)
            throw new InvalidOperationException("You cannot set the same category as parent.");
            
        var ret = await _crudRepository.UpdateAsync(updateDto);

        return ret;
    }
}
