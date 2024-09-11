using Market.Data;
using Market.Dtos;
using Market.Entities;
using Microsoft.EntityFrameworkCore;

namespace Market.Repositories
{
	public class CategoryRepository : ICategoryService
	{
		
		private readonly MarketContext context;
		public CategoryRepository(MarketContext _context)
		{
			this.context = _context;
		}

		// Create: Add a new category
		public async Task<CategoryResponseDto> AddNewCategory(CategoryDto newCategory)
		{
			if (string.IsNullOrWhiteSpace(newCategory.category_name))
			{
				return new CategoryResponseDto
				{
					Is_success = false,
					Message = "Category name cannot be empty"
				};
			}

			if (newCategory.category_name.Length > 50)
			{
				return new CategoryResponseDto
				{
					Is_success = false,
					Message = "Category name must be less than 50 characters"
				};
			}

			bool categoryExists = await context.categories
								 .AnyAsync(c => c.category_name.ToLower() == newCategory.category_name.ToLower());
			if (categoryExists)
			{
				return new CategoryResponseDto
				{
					Is_success = false,
					Message = "Category already exists"
				};
			}

			category newc = new category
			{
				category_name = newCategory.category_name,
				category_description = newCategory.category_description
			};
			await context.categories.AddAsync(newc);
			await context.SaveChangesAsync();
			return new CategoryResponseDto
			{
				Is_success = true,
				Message = "New category has been added successfully",
				category_id = newc.category_id,
				category_name = newc.category_name,
				category_description = newc.category_description
			};
		}

		// Read: Get all categories
		public async Task<List<CategoryResponseDto>> GetAllCategoriesAsync()
		{
			return await context.categories
				.Select(c => new CategoryResponseDto
				{
					category_id = c.category_id,
					category_name = c.category_name,
					category_description = c.category_description
				})
				.ToListAsync();

		}

		// Read: Get category by ID
		public async Task<CategoryResponseDto> GetCategoryById(int id)
		{
			return await context.categories
				.Where(c => c.category_id == id)
				.Select(c => new CategoryResponseDto
				{
					category_id = c.category_id,
					category_name = c.category_name,
					category_description = c.category_description
				})
				.FirstOrDefaultAsync();
		}

		// Update: Edit a category
		public async Task<CategoryResponseDto> EditCategory(CategoryDto category, int id)
		{
			var currentCategory = await context.categories.FirstOrDefaultAsync(x => x.category_id == id);
			if (currentCategory == null)
			{
				return new CategoryResponseDto
				{
					Is_success = false,
					Message = "Category not found"
				};
			}
			if (string.IsNullOrWhiteSpace(category.category_name))
			{
				return new CategoryResponseDto
				{
					Is_success = false,
					Message = "Category name cannot be empty"
				};
			}
			if (category.category_name.Length > 50)
			{
				return new CategoryResponseDto
				{
					Is_success = false,
					Message = "Category name must be less than 50 characters"
				};
			}
			if (string.IsNullOrWhiteSpace(category.category_description))
			{
				return new CategoryResponseDto
				{
					Is_success = false,
					Message = "Category description cannot be empty"
				};
			}
			if (category.category_description.Length > 250)
			{
				return new CategoryResponseDto
				{
					Is_success = false,
					Message = "Category description must be less than 250 characters"
				};
			}

			bool categoryExists = await context.categories
								 .AnyAsync(c => c.category_name.ToLower() == category.category_name.ToLower() && c.category_id != id);
			if (categoryExists)
			{
				return new CategoryResponseDto
				{
					Is_success = false,
					Message = "Another category with the same name already exists"
				};
			}


			currentCategory.category_name = category.category_name;
			currentCategory.category_description = category.category_description;
			await context.SaveChangesAsync();
			return new CategoryResponseDto
			{
				Is_success = true,
				Message = "Category updated successfully",
				category_id = currentCategory.category_id,
				category_name = currentCategory.category_name,
				category_description = currentCategory.category_description
			};
			
		}

		// Delete: Remove a category
		public async Task<Result> DeleteCategory(int id)

		{
			var category = await context.categories.FindAsync(id);
			if (category == null)
			{
				return new Result(false, "Category not found");
			}

			// Check if there are products associated with this category
			bool hasProducts = await context.products.AnyAsync(p => p.category_id == id);
			if (hasProducts)
			{
				return new Result(false, "Cannot delete category because it has associated products");
			}

			context.categories.Remove(category);
			await context.SaveChangesAsync();
			return new Result(true, "Category deleted successfully");
		}
		public async Task<List<CategoryProductsDto>> GetTheProductsOfCategory(int id)
		{
			var category = await context.categories
				.Include(c => c.products)
				.FirstOrDefaultAsync(c => c.category_id == id);

			if (category == null)
			{
				return null;
			}
			if (category.products == null)
			{
				return new List<CategoryProductsDto>();
			}
			return category.products.Select(p => new CategoryProductsDto
			{
				id = p.id,
				name = p.name,
				description = p.description,
				price = p.price,
				stock_quantity = p.stock_quantity,
				image = p.image
			}).ToList();
		}
	}
}
