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
                List<Product> products = context.Products.ToList();
                return Ok(products);
            }
            return BadRequest();
        }
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            Product product = context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
            context.SaveChanges();
        }

        [HttpPost]
        public IActionResult PostProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                 context.Products.Add(product);
                context.SaveChanges();
                return Ok(new { message = "new Product has been added successfully" });
            }
            return BadRequest();

        }
        [HttpPut("{id}")]
        public IActionResult PutProduct(int id, Product product)
        {
            Product OldProduct = context.Products.FirstOrDefault(x => x.Id == id);
            if (ModelState.IsValid)
            {
                if (OldProduct == null)
                {
                    return NotFound();
                }
                product.Name = OldProduct.Name;
                product.Description = OldProduct.Description;
                product.Price = OldProduct.Price;
            }
            return StatusCode(StatusCodes.Status204NoContent);

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            Product product = context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            context.Products.Remove(product);
            context.SaveChanges();
            return BadRequest();
        }
    }
}
