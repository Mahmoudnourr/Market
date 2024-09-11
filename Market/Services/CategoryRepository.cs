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
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            List<Category> categories = await context.Categories.ToListAsync();
            return categories;
        }

        public async Task<Category> GetCategoryById(int id)
        {
            var category = await context.Categories.FirstOrDefaultAsync(x=>x.CategoryId == id);
            return category;
        }

        public async Task<Result> AddNewCategory(Category Newcategory)
        {
            if (string.IsNullOrWhiteSpace(Newcategory.CategoryName))
            {
                return new Result(false, "Category name cannot be empty");
            }
            bool categoryExists = await context.Categories
                                 .AnyAsync(c => c.CategoryName == Newcategory.CategoryName);
            if (categoryExists)
            {
                return new Result(false, "Category already exists ");
            }

            var category = await context.Categories.AddAsync(Newcategory); 
            await context.SaveChangesAsync();
            return new Result(true, "new category has been addded "); 
        }
        public async Task<Result> EditCategory( Category category)
        {
            var CuurentCategory = await context.Categories.FirstOrDefaultAsync(x=>x.CategoryId==category.CategoryId);
            if (CuurentCategory == null)
            {
                return new Result(false, "not found!");
            }
            CuurentCategory.CategoryName=category.CategoryName;
            await context.SaveChangesAsync();
            return new Result(true, "category updated successfully");
        }
        public async Task<Result> DeleteCategory(int id)
        {
            var Category = await context.Categories.FindAsync(id);
            if (Category == null)
            {
                return new Result(false, "not found!");
            }
            context.Categories.Remove(Category);
            await context.SaveChangesAsync();
            return new Result(true, "category deleted successfully"); 

        }
    }
}
