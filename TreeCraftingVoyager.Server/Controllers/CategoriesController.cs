using Microsoft.AspNetCore.Http.HttpResults;
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

        [HttpGet("GetHierarchy")]
        public async Task<IActionResult> GetCategoriesHierarchy()
        {
            var ret = await _crudRepository.GetAllRecursively();

            // mapowanie dodać

            return Ok(ret);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetCategories()
        {
            var ret = await _categoryService.GetAllCategoriesIncludingProducts(); // po co IncludingProducts???

            return Ok(ret);
        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(long id)
        {
            var ret = await _crudRepository.GetByIdAsync(id);

            return Ok(ret);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<CategoryDto>> CreateCategory([FromBody] CreateCategoryDto category)
        {
            if (ModelState.IsValid)
                await _crudRepository.CreateAsync(category);
            else
                return ValidationProblem(ModelState);

            return Ok();
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto category)
        {
            if (ModelState.IsValid)
                await _crudRepository.UpdateAsync(category);
            else
                return ValidationProblem(ModelState);

            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteCategory(long id)
        {
            await _crudRepository.DeleteAsync(id);

            return Ok();
        }
    }
}
