using Market.Dtos;
using Market.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Market.Repositories
{
	public interface IProductService
	{
		public Task<List<GetProductWithCatDto>> GetAllProductsAsync();
		public Task<GetProductWithCatDto> GetProductByIdAsync(int id);
		public Task<Result> AddProductAsync(ProductDto product);
		public Task<Result> UpdateProductAsync(UpdateProductDto product);
		public Task<Result> DeleteProductAsync(int id);
		public Task<Result> UpdateProductPhotoAsync(int id, IFormFile Photo);
	}
}
