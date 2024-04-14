using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeCraftingVoyager.Server.Data.Repositories;
using TreeCraftingVoyager.Server.Data.Repositories.Crud;
using TreeCraftingVoyager.Server.Data.Repositories.Tree;
using TreeCraftingVoyager.Server.Models.Dto.Category;
using TreeCraftingVoyager.Server.Models.Dto.Product;
using TreeCraftingVoyager.Server.Models.Entities;

namespace TreeCraftingVoyager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITreeRepository<Category, CategoryDto, UpdateCategoryDto, CreateCategoryDto> _treeRepository;
        private readonly ICrudRepository<Category, CategoryDto, UpdateCategoryDto, CreateCategoryDto> _crudRepository;

        public CategoriesController(
            IMapper mapper,
            ITreeRepository<Category, CategoryDto, UpdateCategoryDto, CreateCategoryDto> treeRepository,
            ICrudRepository<Category, CategoryDto, UpdateCategoryDto, CreateCategoryDto> crudRepository)
        {
            _mapper = mapper;
            _treeRepository = treeRepository;
            _crudRepository = crudRepository;
        }

        [HttpGet("GetByCategory/{id}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory(long id)
        {
            var productTableName = RepositoryHelpers.GetTableNameByEntityDbName(nameof(Product));
            var ret = await _treeRepository.GetCurrentNodeAndHisChildrensWithLeaves<Product, ProductDto >(id, productTableName);

            return Ok(ret);
        }

        [HttpGet("GetCategories")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetRootCategories()
        {
            var ret = await _treeRepository.GetRootObjects();

            return Ok(ret);
        }

        [HttpGet("GetChildrens/{id}")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategoryChildrens(long id)
        {
            var ret = await _crudRepository.GetQuery(q => q.Where(e => e.Id == id).Include(e => e.Childrens)).SingleOrDefaultAsync();
            if (ret == null)
                return NotFound();

            return Ok(ret);
        }

        [HttpGet("GetHierarchy")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategoriesHierarchy()
        {
            var ret = await _treeRepository.GetAllRecursively();

            return Ok(ret.Where(e => e.ParentId == null));
        }

        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var ret = await _crudRepository.GetAllAsync();

            return Ok(ret);
        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(long id)
        {
            var ret = await _crudRepository.GetByIdAsync(id);
            if (ret == null)
                return NotFound();

            return Ok(ret);
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreateCategory([FromBody] CreateCategoryDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _crudRepository.CreateAsync(createDto);
            if (result == null)
                return StatusCode(500, "Błąd serwera podczas tworzenia kategorii");

            return NoContent();
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _crudRepository.UpdateAsync(updateDto);
            if (result == null)
                return NotFound("Kategoria nie została znaleziona");

            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteCategory(long id)
        {
            var result = await _crudRepository.GetByIdAsync(id);
            if (result == null)
                return NotFound("Kategoria nie została znaleziona");

            await _crudRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
