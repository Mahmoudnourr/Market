using Market.Data;
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
		public async Task<List<category>> GetAllCategoriesAsync()
		{
			List<category> categories = await context.categories.ToListAsync();
			return categories;
		}

		public async Task<category> GetCategoryById(int id)
		{
			var category = await context.categories.FirstOrDefaultAsync(x => x.category_id == id);
			return category;
		}

		public async Task<Result> AddNewCategory(category Newcategory)
		{
			if (string.IsNullOrWhiteSpace(Newcategory.category_name))
			{
				return new Result(false, "Category name cannot be empty");
			}
			bool categoryExists = await context.categories
								 .AnyAsync(c => c.category_name == Newcategory.category_name);
			if (categoryExists)
			{
				return new Result(false, "Category already exists ");
			}

			var category = await context.categories.AddAsync(Newcategory);
			await context.SaveChangesAsync();
			return new Result(true, "new category has been addded ");
		}
		public async Task<Result> EditCategory(category category)
		{
			var CuurentCategory = await context.categories.FirstOrDefaultAsync(x => x.category_id == category.category_id);
			if (CuurentCategory == null)
			{
				return new Result(false, "not found!");
			}
			CuurentCategory.category_name = category.category_name;
			await context.SaveChangesAsync();
			return new Result(true, "category updated successfully");
		}
		public async Task<Result> DeleteCategory(int id)
		{
			var Category = await context.categories.FindAsync(id);
			if (Category == null)
			{
				return new Result(false, "not found!");
			}
			context.categories.Remove(Category);
			await context.SaveChangesAsync();
			return new Result(true, "category deleted successfully");

		}
	}
}
