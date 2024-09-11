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
				List<category> categories = context.categories.ToList();
				return Ok(categories);
			}
			return BadRequest();

		}
		[HttpPost]
		public IActionResult AddCategory([FromBody] category _category)
		{
			if (ModelState.IsValid)
			{
				context.categories.Add(_category);
				context.SaveChanges();
				return Ok(_category);
			}
			return BadRequest();

		}

		[HttpPut("{id}")]
		public IActionResult EditCategory([FromRoute] int id, [FromBody] category category)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}
			var oldCategory = context.categories.FirstOrDefault(x => x.category_id == id);
			if (oldCategory == null)
			{
				return NotFound();
			}
			oldCategory.category_name = category.category_name;
			context.SaveChanges();
			return Ok(oldCategory);

		}
		[HttpDelete("{id}")]
		public IActionResult DeleteCategory(int id)
		{
			var category = context.categories.FirstOrDefault(x => x.category_id == id);

			if (category == null)
			{
				return NotFound();
			}
			context.categories.Remove(category);
			context.SaveChanges();
			return NoContent();
		}
	}
}
