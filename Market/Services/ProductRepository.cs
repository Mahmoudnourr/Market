using Market.Data;
using Market.Dtos;
using Market.Entities;
using Market.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ZstdSharp.Unsafe;

namespace Market.Services
{
	public class ProductRepository : IProductService
	{
		private readonly MarketContext context;
		public ProductRepository(MarketContext _context)
		{
			context = _context;
		}
		public async Task<List<GetProductWithCatDto>> GetAllProductsAsync()
		{
			List<product> products = await context.products.ToListAsync();
			List<GetProductWithCatDto> pros = products
			.Select(p => new GetProductWithCatDto
			{
				id = p.id,
				name = p.name,
				category_id = p.category_id,
				description = p.description,
				image = p.image,
				price = p.price,
				stock_quantity = p.stock_quantity,
			})
		   .ToList();

			return pros;
		}
		public async Task<GetProductWithCatDto> GetProductByIdAsync(int id)
		{
			var product = await context.products.FindAsync(id);

			if (product == null)
			{
				return null;
			}
			var productDto = new GetProductWithCatDto
			{
				description = product.description,
				name = product.name,
				price = product.price,
				image = product.image,
				category_id = product.category_id,
				id = id,
				stock_quantity = product.stock_quantity,
			};

			return productDto;
		}
		public async Task<Result> AddProductAsync(ProductDto pr)
		{
			var res = await context.products.AnyAsync(e => e.name == pr.Name);
			var res2 = await context.categories.AnyAsync(e => e.category_id == pr.CategoryId);
			if (!res2)
			{
				return new Result(false, "The Category Is not Found");
			}
			if (res)
			{
				return new Result(false, "The Name Of the product is already found");
			}

			var str = new MemoryStream();
			await pr.img.CopyToAsync(str);
			var product = new product
			{
				name = pr.Name,
				price = pr.Price,
				description = pr.Description,
				category_id = pr.CategoryId,
				image = str.ToArray(),
				stock_quantity = pr.stock_quantity,
			};
			await context.products.AddAsync(product);
			try
			{
				await context.SaveChangesAsync();
				return new Result(true, "new product has been added succcefully");
			}
			catch (Exception ex)
			{
				return new Result(false, ex.Message);
			}
		}
		public async Task<Result> UpdateProductAsync(UpdateProductDto productDto)
		{
			var Currentproduct = await context.products.FirstOrDefaultAsync(x => x.id == productDto.Id);
			if (Currentproduct == null)
			{
				return new Result(false, "product not found");
			}
			Currentproduct.price = productDto.Price;
			Currentproduct.stock_quantity = productDto.stock_quantity;
			Currentproduct.description = productDto.Description;
			Currentproduct.name = productDto.Name;
			try
			{
				await context.SaveChangesAsync();
				return new Result(true, "product has been updated succefully ");
			}
			catch (Exception ex)
			{
				return new Result(false, ex.Message);
			}
		}
		public async Task<Result> DeleteProductAsync(int id)
		{
			product product = await context.products.FindAsync(id);
			if (product == null)
			{
				return new Result(false, "product not found");
			}
			context.products.Remove(product);
			await context.SaveChangesAsync();
			return new Result(true, "product has been removed succesfully");
		}

		public async Task<Result> UpdateProductPhotoAsync(int id, IFormFile Photo)
		{
			product pr
				= await context.products.FindAsync(id);
			if (pr is null)
			{
				return new Result(false, "The Product Not Found");
			}
			else
			{
				var str = new MemoryStream();
				await Photo.CopyToAsync(str);
				pr.image = str.ToArray();
				await context.SaveChangesAsync();
				return new Result(true, "The Image Updated Scuccefully ");
			}
		}
	}
}
