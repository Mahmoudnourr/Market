using Market.Dtos;
using Market.Entities;

namespace Market.Repositories
{
	public interface ICategoryService
	{
		public Task<List<CategoryResponseDto>> GetAllCategoriesAsync();
		public Task<CategoryResponseDto> GetCategoryById(int id);
		public Task<CategoryResponseDto> AddNewCategory(CategoryDto category);
		public Task<CategoryResponseDto> EditCategory(CategoryDto category,int id);
		public Task<Result> DeleteCategory(int id);
		public Task<List<CategoryProductsDto>> GetTheProductsOfCategory(int id);
	}
}
