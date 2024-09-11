using Market.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Market.Repositories
{
    public interface IProductService
    {
       public Task<List<ProductDto>> GetAllProductsAsync();
        public Task<ProductDto> GetProductByIdAsync(int id);
        public  Task<Result> AddProductAsync(ProductDto product);
        public  Task<Result> UpdateProductAsync( ProductDto product);
        public  Task<Result> DeleteProductAsync(int id);

    }
}
