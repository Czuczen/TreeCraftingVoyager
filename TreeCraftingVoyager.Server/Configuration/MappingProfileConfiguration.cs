using AutoMapper;
using TreeCraftingVoyager.Server.Models.Dto.Category;
using TreeCraftingVoyager.Server.Models.Dto.Product;
using TreeCraftingVoyager.Server.Models.Entities;
using TreeCraftingVoyager.Server.Models.ViewModels.Category;
using TreeCraftingVoyager.Server.Models.ViewModels.Product;

namespace TreeCraftingVoyager.Server.Configuration;

public class MappingProfileConfiguration : Profile
{
    public MappingProfileConfiguration()
    {
        // EntityBase => EntityDto
        CreateMap<Category, CategoryDto>();
        CreateMap<Product, ProductDto>();



        // EntityBase => ViewModel
        CreateMap<Product, ProductDetailsViewModel>();


        // CreateEntityDto => EntityBase
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<CreateProductDto, Product>();


        // EntityDto => ViewModel
        CreateMap<CategoryDto, CategoryDetailsViewModel>();
        CreateMap<ProductDto, ProductDetailsViewModel>();


        



    }
}
