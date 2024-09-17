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
}

