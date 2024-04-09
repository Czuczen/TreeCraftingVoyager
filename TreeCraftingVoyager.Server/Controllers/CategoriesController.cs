using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeCraftingVoyager.Server.Models.Dto.Category;
using TreeCraftingVoyager.Server.Models.Entities;

namespace TreeCraftingVoyager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        // Symulacja bazy danych za pomocą statycznej listy
        private static List<Category> _categories = new List<Category>();
        private static long _nextId = 1;


        public CategoriesController()
        {
            var category1 = new Category
            {
                Id = _nextId++,
                Name = "Aaaa",
                Description = "Aaaa",
                ImageURL = "Aaaa",
                SEOKeywords = "Aaaa",
                IsActive = true
            };

            var category2 = new Category
            {
                Id = _nextId++,
                Name = "Bbbb",
                Description = "Bbbb",
                ImageURL = "Bbbb",
                SEOKeywords = "Bbbb",
                IsActive = false
            };

            var category3 = new Category
            {
                Id = _nextId++,
                Name = "Cccc",
                Description = "Cccc",
                ImageURL = "Cccc",
                SEOKeywords = "Cccc",
                IsActive = true
            };

            _categories.Add(category1);
            _categories.Add(category2);
            _categories.Add(category3);
        }


        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(_categories);
        }

        [HttpGet("GetCategory/{id}")]
        public async Task<ActionResult<Category>> GetCategory(long id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return await Task.FromResult<ActionResult<Category>>(NotFound());
            }

            return await Task.FromResult<ActionResult<Category>>(category);
        }

        [HttpPost("CreateCategory")]
        public async Task<ActionResult<Category>> CreateCategory([FromBody] CreateCategoryDto category)
        {
            var categoryqqq = new Category();
            categoryqqq.Id = _nextId++;
            _categories.Add(categoryqqq);

            return await Task.FromResult<ActionResult<Category>>(CreatedAtAction("GetCategory", new { id = categoryqqq.Id }, categoryqqq));
        }

        [HttpPut("PutCategory/{id}")]
        public async Task<IActionResult> PutCategory(long id, Category category)
        {
            var index = _categories.FindIndex(c => c.Id == id);
            if (index == -1)
            {
                return await Task.FromResult<IActionResult>(NotFound());
            }

            category.Id = id;
            _categories[index] = category;

            return await Task.FromResult<IActionResult>(NoContent());
        }

        [HttpDelete("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(long id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return await Task.FromResult<IActionResult>(NotFound());
            }

            _categories.Remove(category);

            return await Task.FromResult<IActionResult>(NoContent());
        }
    }
}
