using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeCraftingVoyager.Server.Data.Repositories.Crud;
using TreeCraftingVoyager.Server.Models.Dto.Category;
using TreeCraftingVoyager.Server.Models.Entities;
using TreeCraftingVoyager.Server.Services;

namespace TreeCraftingVoyager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ICrudRepository<Category, CategoryDto, UpdateCategoryDto, CreateCategoryDto> _crudRepository;

        public CategoriesController(
            ICategoryService categoryService,
            ICrudRepository<Category, CategoryDto, UpdateCategoryDto, CreateCategoryDto> crudRepository
            )
        {
            _categoryService = categoryService;
            _crudRepository = crudRepository;
        }


        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            var ret = await _categoryService.GetAllCategoriesIncludingProducts();

            return Ok(ret);
        }

        [HttpGet("GetCategory/{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(long id)
        {
            var ret = await _crudRepository.GetByIdAsync(id);

            return Ok(ret);
        }

        [HttpPost("CreateCategory")]
        public async Task<ActionResult<CategoryDto>> CreateCategory([FromBody] CreateCategoryDto category)
        {
            var ret = await _crudRepository.CreateAsync(category);

            return Ok(ret);
        }

        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto category)
        {
            var ret = await _crudRepository.UpdateAsync(category);

            return Ok(ret);
        }

        [HttpDelete("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(long id)
        {
            await _crudRepository.DeleteAsync(id);

            return Ok();
        }
    }
}
