﻿using TreeCraftingVoyager.Server.Configuration.Dependencies.DependencyLifecycleInterfaces;
using TreeCraftingVoyager.Server.Models.Dto.Category;

namespace TreeCraftingVoyager.Server.Services.CategoryService;

public interface ICategoryService : ITransientDependency
{
    Task<CategoryDto?> GetCategoryDetails(long id);

    Task<CategoryDto> UpdateCategory(UpdateCategoryDto updateDto);
}