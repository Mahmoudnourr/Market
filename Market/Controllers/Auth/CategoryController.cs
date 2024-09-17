using Market.Data;
using Market.Entities;
using Market.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
	/*<<<<<<< HEAD
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
	=======*/
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private ICategoryService categoryService;
		public CategoryController(ICategoryService _categryService)
		{
			categoryService = _categryService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllCategories()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var categories = await categoryService.GetAllCategoriesAsync();
			return Ok(categories);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetCategoryById(int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var category = await categoryService.GetCategoryById(id);
			if (category == null)
			{
				return NotFound();
			}
			return Ok(category);
		}

		[HttpPost]
		public async Task<IActionResult> AddCategory([FromBody] category NewCategory)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var category = await categoryService.AddNewCategory(NewCategory);
			return Ok(category);
		}

		[HttpPut]
		public async Task<IActionResult> EditCategory([FromBody] category category)
		{
			var CuuretCategory = await categoryService.EditCategory(category);
			if (CuuretCategory == null)
			{
				return BadRequest();
			}
			return Ok(CuuretCategory);
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCategory(int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var category = await categoryService.DeleteCategory(id);
			if (category == null)
			{
				return NotFound("Category not found");
			}
			return Ok();
		}
	}
	//>>>>>>> 86623786be269c31a0bb4789d5cb7977c5f04491
}
