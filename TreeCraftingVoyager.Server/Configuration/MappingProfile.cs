using AutoMapper;
using TreeCraftingVoyager.Server.Models.Dto.Category;
using TreeCraftingVoyager.Server.Models.Dto.Product;
using TreeCraftingVoyager.Server.Models.Entities;
using TreeCraftingVoyager.Server.Models.ViewModels;

namespace TreeCraftingVoyager.Server.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // EntityBase => EntityDto
            CreateMap<Category, CategoryDto>();
            CreateMap<Product, ProductDto>();




            // CreateEntityDto => EntityBase
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<CreateProductDto, Product>();



            //CreateMap<CategoryDto, CategoryViewModel>();
        }
    }
}
