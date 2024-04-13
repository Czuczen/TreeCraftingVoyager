using Microsoft.EntityFrameworkCore;
using TreeCraftingVoyager.Server.Data.Repositories.Crud;
using TreeCraftingVoyager.Server.Models.Dto.Category;
using TreeCraftingVoyager.Server.Models.Entities;

namespace TreeCraftingVoyager.Server.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICrudRepository<Category, CategoryDto, UpdateCategoryDto, CreateCategoryDto> _crudRepository;

        public CategoryService(ICrudRepository<Category, CategoryDto, UpdateCategoryDto, CreateCategoryDto> crudRepository)
        {
            _crudRepository = crudRepository;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesIncludingProducts()
        {
            //return await _crudRepository.GetQuery(q => q.Include(c => c.Products)).ToListAsync();
            return null;
        }
    }
}
