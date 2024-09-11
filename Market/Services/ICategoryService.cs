using Market.Entities;

namespace Market.Repositories
{
    public interface ICategoryService
    {
        public Task<List<Category>> GetAllCategoriesAsync();
        public Task<Category> GetCategoryById(int id);
        public Task<Result> AddNewCategory(Category category);
        public Task<Result> EditCategory (Category category);
        public Task<Result> DeleteCategory(int id);
    }
}
