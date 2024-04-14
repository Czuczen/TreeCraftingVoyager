using TreeCraftingVoyager.Server.Data.Repositories.Crud;
using TreeCraftingVoyager.Server.Models.Dto.Product;
using TreeCraftingVoyager.Server.Models.Entities;

namespace TreeCraftingVoyager.Server.Services.ProductService;

public class ProductService : IProductService
{
    private readonly ICrudRepository<Product, ProductDto, UpdateProductDto, CreateProductDto> _crudRepository;


    public ProductService(
        ICrudRepository<Product, ProductDto, UpdateProductDto, CreateProductDto> crudRepository)
    {
            _crudRepository = crudRepository;
        }



}