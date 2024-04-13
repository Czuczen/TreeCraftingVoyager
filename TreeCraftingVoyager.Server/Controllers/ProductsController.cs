using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TreeCraftingVoyager.Server.Data.Repositories.Crud;
using TreeCraftingVoyager.Server.Data.Repositories.Tree;
using TreeCraftingVoyager.Server.Models.Dto.Product;
using TreeCraftingVoyager.Server.Models.Entities;

namespace TreeCraftingVoyager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICrudRepository<Product, ProductDto, UpdateProductDto, CreateProductDto> _crudRepository;

        public ProductsController(
            IMapper mapper,
            ICrudRepository<Product, ProductDto, UpdateProductDto, CreateProductDto> crudRepository
            )
        {
            _mapper = mapper;
            _crudRepository = crudRepository;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetProducts()
        {
            var ret = await _crudRepository.GetAllAsync(); 

            return Ok(ret);
        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(long id)
        {
            var ret = await _crudRepository.GetByIdAsync(id);

            return Ok(ret);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] CreateProductDto product)
        {
            if (ModelState.IsValid)
                await _crudRepository.CreateAsync(product);
            else
                return ValidationProblem(ModelState);

            return Ok();
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto product)
        {
            if (ModelState.IsValid)
                await _crudRepository.UpdateAsync(product);
            else
                return ValidationProblem(ModelState);

            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            await _crudRepository.DeleteAsync(id);

            return Ok();
        }
    }
}

