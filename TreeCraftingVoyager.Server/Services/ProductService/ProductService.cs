using Microsoft.EntityFrameworkCore;
using TreeCraftingVoyager.Server.Data.Repositories.Crud;
using TreeCraftingVoyager.Server.Models.Dto.Category;
using TreeCraftingVoyager.Server.Models.Dto.Product;
using TreeCraftingVoyager.Server.Models.Entities;

namespace TreeCraftingVoyager.Server.Services.ProductService;

public class ProductService : IProductService
{
    private readonly ICrudRepository<Product, ProductDto, UpdateProductDto, CreateProductDto> _crudRepository;


    public ProductService(ICrudRepository<Product, ProductDto, UpdateProductDto, CreateProductDto> crudRepository)
    {
            _crudRepository = crudRepository;
    }

    public async Task<ProductDto?> GetProductDetails(long id)
    {
        var ret = await _crudRepository.GetQuery(q => q
            .Include(e => e.Category)
            .Where(e => e.Id == id))
            .SingleOrDefaultAsync();

        return ret;
    }

    
}
