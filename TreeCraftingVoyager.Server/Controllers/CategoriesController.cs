using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpPost("PostCategory")]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            category.Id = _nextId++;
            _categories.Add(category);

            return await Task.FromResult<ActionResult<Category>>(CreatedAtAction("GetCategory", new { id = category.Id }, category));
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
