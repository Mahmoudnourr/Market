using Market.Data;
using Market.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Market.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly MarketContext context;
        public CategoryController(MarketContext _context)
        {
            this.context = _context;
        }
        [HttpGet]
        public IActionResult GetCategory()
        {
            if (ModelState.IsValid)
            {
                List<Category> categories = context.Categories.ToList();
                return Ok(categories);
            }
            return BadRequest();

        }
        [HttpPost]
        public IActionResult AddCategory([FromBody] Category category)
        {
            if (ModelState.IsValid)
            {
                context.Categories.Add(category);
                context.SaveChanges();
                return Ok(category);
            }
            return BadRequest();

        }

        [HttpPut("{id}")]
        public IActionResult EditCategory([FromRoute] int id, [FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var oldCategory = context.Categories.FirstOrDefault(x => x.CategoryId == id);
            if (oldCategory == null)
            {
                return NotFound(); 
            }
            oldCategory.CategoryName = category.CategoryName;
            context.SaveChanges();
            return Ok(oldCategory);

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = context.Categories.FirstOrDefault(x => x.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }
            context.Categories.Remove(category);
            context.SaveChanges();
            return NoContent();
        }
    }
}
