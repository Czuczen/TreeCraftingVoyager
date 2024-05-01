using TreeCraftingVoyager.Server.Configuration.Dependencies.DependencyLifecycleInterfaces;
using TreeCraftingVoyager.Server.Models.Dto.Product;

namespace TreeCraftingVoyager.Server.Services.ProductService;

public interface IProductService : ITransientDependency
{
    Task<ProductDto?> GetProductDetails(long id);
}