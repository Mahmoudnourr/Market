using Market.Data;
using Market.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ZstdSharp.Unsafe;

namespace Market.Repositories
{
    public class ProductRepository : IProductService
    {
        private readonly MarketContext context;
        public ProductRepository(MarketContext _context)
        {
            this.context = _context;
        }
        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            //List<ProductDto> products = await context.Products.ToListAsync();
            //return products;
                var products = await context.Products.ToListAsync();
                var productDtos = products.Select(product => new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                }).ToList();

                return productDtos;
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await context.Products.FindAsync(id);

            if (product == null)
            {
                return null; 
            }
            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
            };

            return productDto;
        }
        public async Task<Result> AddProductAsync(ProductDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Description = productDto.Description,
                CategoryId = productDto.CategoryId
            };
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            return new Result(true, "new product has been added succcefully");
        }
        public async Task<Result> UpdateProductAsync(ProductDto productDto)
        {
            var Currentproduct = await context.Products.FirstOrDefaultAsync(x=>x.Id==productDto.Id);
            if (Currentproduct == null)
            {
                return new Result(false, "product not found");
            }
            context.Entry(Currentproduct).CurrentValues.SetValues(productDto);
             await context.SaveChangesAsync();
            return new Result(true, "product has been updated succefully ");
        }
        public async Task<Result> DeleteProductAsync(int id)
        {
            Product product = await context.Products.FindAsync(id);
            if(product==null)
            {
                return new Result(false, "product not found"); 
            }
            context.Products.Remove(product);
            await context.SaveChangesAsync();
            return new Result(true, "product has been removed succesfully");
        }

    }
}
