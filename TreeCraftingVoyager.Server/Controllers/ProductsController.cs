using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TreeCraftingVoyager.Server.Data.Repositories.Crud;
using TreeCraftingVoyager.Server.Models.Dto.Product;
using TreeCraftingVoyager.Server.Models.Entities;
using TreeCraftingVoyager.Server.Models.ViewModels.Category;
using TreeCraftingVoyager.Server.Models.ViewModels.Product;
using TreeCraftingVoyager.Server.Services.ProductService;

namespace TreeCraftingVoyager.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IProductService _productService;
    private readonly ICrudRepository<Product, ProductDto, UpdateProductDto, CreateProductDto> _crudRepository;

    public ProductsController(
        IMapper mapper,
        IProductService productService,
        ICrudRepository<Product, ProductDto, UpdateProductDto, CreateProductDto> crudRepository)
    {
        _mapper = mapper;
        _productService = productService;
        _crudRepository = crudRepository;
    }

    [HttpGet("Get")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
    {
        var ret = await _crudRepository.GetAllAsync();

        return Ok(ret);
    }

    [HttpGet("Details/{id}")]
    public async Task<ActionResult<ProductDto>> GetProductDetails(long id)
    {
        var ret = await _productService.GetProductDetails(id);
        if (ret == null)
            return NotFound();

        return Ok(_mapper.Map<ProductDetailsViewModel>(ret));
    }

    [HttpGet("Get/{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(long id)
    {
        var ret = await _crudRepository.GetByIdAsync(id);
        if (ret == null)
            return NotFound();

        return Ok(ret);
    }

    [Authorize]
    [HttpPost("Create")]
    public async Task<ActionResult> CreateProduct([FromBody] CreateProductDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _crudRepository.CreateAsync(createDto);
        if (result == null)
            return StatusCode(500, "Błąd serwera podczas tworzenia produktu");

        return NoContent();
    }

    [Authorize]
    [HttpPut("Update")]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _crudRepository.UpdateAsync(updateDto);
        if (result == null)
            return NotFound("Produkt nie został znaleziony");

        return NoContent();
    }

    [Authorize]
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteProduct(long id)
    {
        var result = await _crudRepository.GetByIdAsync(id);
        if (result == null)
            return NotFound("Produkt nie został znaleziony");

        await _crudRepository.DeleteAsync(id);

        return NoContent();
    }
}
