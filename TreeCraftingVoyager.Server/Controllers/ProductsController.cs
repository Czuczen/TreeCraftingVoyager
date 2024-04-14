﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TreeCraftingVoyager.Server.Data.Repositories.Crud;
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
            ICrudRepository<Product, ProductDto, UpdateProductDto, CreateProductDto> crudRepository)
        {
            _mapper = mapper;
            _crudRepository = crudRepository;
        }

        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var ret = await _crudRepository.GetAllAsync();

            return Ok(ret);
        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(long id)
        {
            var ret = await _crudRepository.GetByIdAsync(id);
            if (ret == null)
                return NotFound();

            return Ok(ret);
        }

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
}