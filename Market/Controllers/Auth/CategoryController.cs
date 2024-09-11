using Market.Data;
using Market.Dtos;
using Market.Entities;
using Market.Entities;
using Market.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private ICategoryService categoryService;
		public CategoryController(ICategoryService _categryService)
		{
			categoryService = _categryService;
		}

		[HttpGet("GetAllCategories")]
		public async Task<IActionResult> GetAllCategories()
		{
			var categories = await categoryService.GetAllCategoriesAsync();
			if (categories == null)
			{
				return NotFound("No Categories Added ");
			}
			else
				return Ok(categories);
		}
		[HttpGet("GetCategoryById")]
		public async Task<IActionResult> GetCategoryById(int id)
		{
			var category = await categoryService.GetCategoryById(id);
			if (category != null)
			{
				return Ok(category);
			}
			else
				return BadRequest("Category not found");
		}

		[HttpPost("AddCategory")]
		public async Task<IActionResult> AddCategory([FromBody]  CategoryDto NewCategory)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var result = await categoryService.AddNewCategory(NewCategory);
			if (result.Is_success)
			{
				return Ok(result);
			}
			return BadRequest(result.Message);
		}

		[HttpPut("EditCategory")]
		public async Task<IActionResult> EditCategory([FromBody] CategoryDto category,int id)
		{
			var result = await categoryService.EditCategory(category,id);
			if (!result.Is_success)
			{
				return BadRequest(result.Message);

			}
			return Ok(result);
		}
		[HttpDelete("DeleteCategory")]
		
		public async Task<IActionResult> DeleteCategory(int id)
		{
			var result = await categoryService.DeleteCategory(id);
			if (result.IsSuccess)
			{
				return Ok(result);
			}
			return BadRequest(result.Message);
		}
		[HttpGet("GetTheProductsOfCategory")]
		public async Task<IActionResult> GetTheProductsOfCategory(int id)
		{
			var products = await categoryService.GetTheProductsOfCategory(id);
			if (products == null)
			{
				return NotFound("Category not found");
			}
			if (products.Count == 0)
			{
				return NotFound("No products found for this category");
			}
			else
				return Ok(products);
		}
	}

}
