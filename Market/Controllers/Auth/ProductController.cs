using Market.Data;
using Market.Dtos;
using Market.Entities;
using Market.Repositories;
using Market.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Market.Controllers
{
	/*<<<<<<< HEAD
		[Route("api/[controller]")]
		[ApiController]
		public class ProductController : ControllerBase
		{
			private readonly MarketContext context;
			public ProductController(MarketContext _context)
			{
				context = _context;
			}
			[HttpGet]
			public IActionResult GetProducts()
			{
				if (ModelState.IsValid)
				{
					List<product> products = context.products.ToList();
					return Ok(products);
				}
				return BadRequest();
			}
			[HttpGet("{id}")]
			public IActionResult GetProductById(int id)
			{
				product product = context.products.FirstOrDefault(x => x.id == id);
				if (product == null)
				{
					return NotFound();
				}
				return Ok(product);
				context.SaveChanges();
			}

			[HttpPost]
			public IActionResult PostProduct(product product)
			{
				if (ModelState.IsValid)
				{
					context.products.Add(product);
					context.SaveChanges();
					return Ok(new { message = "new Product has been added successfully" });
				}
				return BadRequest();

			}
			[HttpPut("{id}")]
			public IActionResult PutProduct(int id, product product)
			{
				product OldProduct = context.products.FirstOrDefault(x => x.id == id);
				if (ModelState.IsValid)
				{
					if (OldProduct == null)
					{
						return NotFound();
					}
					product.name = OldProduct.name;
					product.description = OldProduct.description;
					product.price = OldProduct.price;
				}
				return StatusCode(StatusCodes.Status204NoContent);

			}
			[HttpDelete("{id}")]
			public IActionResult DeleteProduct(int id)
			{
				product product = context.products.FirstOrDefault(x => x.id == id);
				if (product == null)
				{
					return NotFound();
				}
				context.products.Remove(product);
				context.SaveChanges();
				return BadRequest();
			}
		}
	=======*/
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private IProductService productService;
		public ProductController(IProductService _productService)
		{
			this.productService = _productService;
		}
		[HttpGet]
		//[Authorize(Policy = "Customer")]
		public async Task<IActionResult> GetAllProducts()
		{
			var products = await productService.GetAllProductsAsync();
			if (products == null)
			{
				return BadRequest("No Products has been Created yet");
			}
			return Ok(products);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetProductById(int id)
		{
			var product = await productService.GetProductByIdAsync(id);
			if (product == null)
			{
				return BadRequest("The Product Is not Found");
			}
			return Ok(product);
		}
		[HttpPost("Add Product")]
		public async Task<IActionResult> AddNewProduct([FromForm] ProductDto productDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var result = await productService.AddProductAsync(productDto);
			if (result.IsSuccess)
			{
				return Ok(result);
			}
			return BadRequest(result.Message);
		}
		[HttpPut("Update Product")]
		public async Task<IActionResult> EditProduct([FromForm] UpdateProductDto productDto)
		{
			var result = await productService.UpdateProductAsync(productDto);
			if (!result.IsSuccess)
			{
				return BadRequest(result.Message);
			}
			return Ok(result.Message);

		}
		[HttpDelete("Delete Product")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			var result = await productService.DeleteProductAsync(id);
			if (!result.IsSuccess)
			{
				return NotFound(result.Message);
			}
			return Ok(result.Message);
		}
		[HttpPut("Update Photo")]
		public async Task<IActionResult> UpdatePhoto([FromForm][Required(ErrorMessage = "The Id Is required")] int id,
		[Required(ErrorMessage = "Please select a product image.")]
		[DataType(DataType.Upload)]
		[AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" }, ErrorMessage = "Only JPG, JPEG, and PNG files are allowed.")]
		[MaxFileSize(10 * 1024 * 1024, ErrorMessage = "Maximum allowed file size is 10 MB.")]
			IFormFile photo)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			else
			{
				var res = await productService.UpdateProductPhotoAsync(id, photo);
				if (!res.IsSuccess)
				{
					return BadRequest(res.Message);
				}
				return Ok(res);
			}

		}
	}
	//>>>>>>> 86623786be269c31a0bb4789d5cb7977c5f04491
}

