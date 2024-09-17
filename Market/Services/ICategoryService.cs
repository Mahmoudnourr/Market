using Market.Entities;

namespace Market.Repositories
{
	public interface ICategoryService
	{
		public Task<List<category>> GetAllCategoriesAsync();
		public Task<category> GetCategoryById(int id);
		public Task<Result> AddNewCategory(category category);
		public Task<Result> EditCategory(category category);
		public Task<Result> DeleteCategory(int id);
	}
}
