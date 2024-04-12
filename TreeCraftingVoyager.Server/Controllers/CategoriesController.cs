using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TreeCraftingVoyager.Server.Data.Repositories.Crud;
using TreeCraftingVoyager.Server.Data.Repositories.Tree;
using TreeCraftingVoyager.Server.Models.Dto.Category;
using TreeCraftingVoyager.Server.Models.Entities;
using TreeCraftingVoyager.Server.Models.ViewModels;
using TreeCraftingVoyager.Server.Services;

namespace TreeCraftingVoyager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly ITreeRepository<Category, CategoryDto, UpdateCategoryDto, CreateCategoryDto> _treeRepository;
        private readonly ICrudRepository<Category, CategoryDto, UpdateCategoryDto, CreateCategoryDto> _crudRepository;

        public CategoriesController(
            IMapper mapper,
            ICategoryService categoryService,
            ITreeRepository<Category, CategoryDto, UpdateCategoryDto, CreateCategoryDto> treeRepository,
            ICrudRepository<Category, CategoryDto, UpdateCategoryDto, CreateCategoryDto> crudRepository
            )
        {
            _mapper = mapper;
            _categoryService = categoryService;
            _treeRepository = treeRepository;
            _crudRepository = crudRepository;
        }


        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetRootCategories()
        {
            var ret = await _treeRepository.GetRootObjects();

            return Ok(ret);
        }

        [HttpGet("GetChildrens/{id}")]
        public async Task<IActionResult> GetCategoryChildrens(long id)
        {
            var ret = await _crudRepository.GetQuery(q => q.Where(e => e.Id == id).Include(e => e.Childrens)).SingleAsync();

            return Ok(ret.Childrens);
        }

        [HttpGet("GetHierarchy")]
        public async Task<IActionResult> GetCategoriesHierarchy()
        {
            var ret = await _treeRepository.GetAllRecursively();
            
            return Ok(ret.Where(e => e.ParentId == null));
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
