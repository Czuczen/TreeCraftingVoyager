using TreeCraftingVoyager.Server.Configuration.Dependencies.DependencyLifecycleInterfaces;
using TreeCraftingVoyager.Server.Models.Dto.Category;

namespace TreeCraftingVoyager.Server.Services
{
    public interface ICategoryService : ITransientDependency
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesIncludingProducts();
    }
}
