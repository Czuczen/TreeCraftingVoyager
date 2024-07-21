using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TreeCraftingVoyager.Server.Attributes.Filter;
using TreeCraftingVoyager.Server.Data.Repositories.Tree;
using TreeCraftingVoyager.Server.Models.Dto.Category;
using TreeCraftingVoyager.Server.Models.Dto.Product;
using TreeCraftingVoyager.Server.Models.Entities;
using TreeCraftingVoyager.Server.Models.ViewModels.Category;
using TreeCraftingVoyager.Server.Models.ViewModels.Product;
using TreeCraftingVoyager.Server.Services.CategoryService;

namespace TreeCraftingVoyager.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICategoryService _categoryService;
    private readonly ITreeRepository<Category, CategoryDto, UpdateCategoryDto, CreateCategoryDto> _treeRepository;

    public CategoriesController(
        IMapper mapper,
        ICategoryService categoryService,
        ITreeRepository<Category, CategoryDto, UpdateCategoryDto, CreateCategoryDto> treeRepository)
    {
        _mapper = mapper;
        _categoryService = categoryService;
        _treeRepository = treeRepository;
    }


    [HttpGet("GetByCategory/{id}")]
    [RateLimit(100, 60)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory(long id)
    {
        var ret = await _treeRepository.GetCurrentNodeAndHisChildrensWithLeaves<Product>(id).Include(e => e.Category).ToListAsync();

        return Ok(_mapper.Map<IEnumerable<ProductDetailsViewModel>>(ret));
    }

    [HttpGet("GetCategories")]
    [RateLimit(100, 60)]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetRootCategories()
    {
        var ret = await _treeRepository.GetRootObjects();

        return Ok(ret);
    }

    [HttpGet("GetChildrens/{id}")]
    [RateLimit(100, 60)]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategoryChildrens(long id)
    {
        var ret = await _treeRepository.GetQuery(q => q.Where(e => e.Id == id).Include(e => e.Childrens)).SingleOrDefaultAsync();
        if (ret == null)
            return NotFound();

        return Ok(ret);
    }

    [HttpGet("GetHierarchy")]
    [RateLimit(100, 60)]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategoriesHierarchy()
    {
        var ret = await _treeRepository.GetAllRecursively();

        return Ok(ret.Where(e => e.ParentId == null));
    }

    [HttpGet("Get")]
    [RateLimit(100, 60)]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
    {
        var ret = (await _treeRepository.GetAllAsync()).OrderBy(e => e.Name);

        return Ok(ret);
    }

    [HttpGet("Details/{id}")]
    [RateLimit(100, 60)]
    public async Task<ActionResult<CategoryDto>> GetCategoryDetails(long id)
    {
        var ret = await _categoryService.GetCategoryDetails(id);
        if (ret == null)
            return NotFound();

        return Ok(_mapper.Map<CategoryDetailsViewModel>(ret));
    }

    [HttpGet("Get/{id}")]
    [RateLimit(100, 60)]
    public async Task<ActionResult<ProductDto>> GetCategory(long id)
    {
        var ret = await _treeRepository.GetByIdAsync(id);
        if (ret == null)
            return NotFound();

        return Ok(ret);
    }

    [Authorize]
    [HttpPost("Create")]
    [RateLimit(100, 60)]
    public async Task<ActionResult> CreateCategory([FromBody] CreateCategoryDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _treeRepository.CreateAsync(createDto);
        if (result == null)
            return StatusCode(500, "Błąd serwera podczas tworzenia kategorii");

        return NoContent();
    }

    [Authorize]
    [HttpPut("Update")]
    [RateLimit(100, 60)]
    public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _categoryService.UpdateCategory(updateDto);
        if (result == null)
            return NotFound("Kategoria nie została znaleziona");

        return NoContent();
    }

    [Authorize]
    [HttpDelete("Delete/{id}")]
    [RateLimit(100, 60)]
    public async Task<IActionResult> DeleteCategory(long id)
    {
        var result = await _treeRepository.GetByIdAsync(id);
        if (result == null)
            return NotFound("Kategoria nie została znaleziona");

        await _treeRepository.DeleteAsync(id);

        return NoContent();
    }
}
