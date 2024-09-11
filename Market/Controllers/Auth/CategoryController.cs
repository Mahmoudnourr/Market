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
        public async Task<IActionResult> AddCategory([FromBody] Category NewCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                var category = await categoryService.AddNewCategory(NewCategory);
                return Ok(category);
        }

        [HttpPut]
        public async Task<IActionResult> EditCategory([FromBody] Category category)
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
}
