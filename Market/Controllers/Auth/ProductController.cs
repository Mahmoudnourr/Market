using Market.Data;
using Market.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Market.Controllers
{
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
}
