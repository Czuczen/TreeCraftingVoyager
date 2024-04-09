using AutoMapper;
using TreeCraftingVoyager.Server.Models.Dto.Category;
using TreeCraftingVoyager.Server.Models.Entities;

namespace TreeCraftingVoyager.Server.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // EntityBase => EntityDto
            CreateMap<Category, CategoryDto>();




            // CreateEntityDto => EntityBase
            CreateMap<CreateCategoryDto, Category>();
        }
    }
}
