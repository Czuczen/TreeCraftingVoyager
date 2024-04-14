using AutoMapper;

namespace TreeCraftingVoyager.Server.Configuration;

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