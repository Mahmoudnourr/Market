using Market.Data;
using Market.Entities;
using Market.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Market.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private  IProductService productService;
        public ProductController(IProductService _productService)
        {
            this.productService = _productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await productService.GetAllProductsAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById([FromRoute]int id)
        {
            var product = await productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> AddNewProduct([FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
        
            var result = await productService.AddProductAsync(productDto);

            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> EditProduct([FromBody] ProductDto productDto)
        {
            var product = await productService.UpdateProductAsync(productDto);
            if (product == null)
            {
                return BadRequest("not updating");
            }
            return Ok(product);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute]int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var product = await productService.DeleteProductAsync(id);
            if (product == null)
            {
                return NotFound("product not found"); 
            }
            return Ok();
        }
    }
}

