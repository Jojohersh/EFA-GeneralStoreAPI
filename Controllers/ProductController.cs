using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneralStore.Data;
using GeneralStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeneralStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private GeneralStoreDbContext _db;
        public ProductController(GeneralStoreDbContext db)
        {
            _db = db;
        }


        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] ProductEdit newProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(newProduct);
            }

            Product product = new Product()
            {
                Name = newProduct.Name,
                Price = newProduct.Price,
                QuantityInStock = newProduct.Quantity
            };

            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _db.Products.ToListAsync();
            return Ok(products);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product is null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductEdit updatedProduct)
        {
            var productInDb = await _db.Products.FindAsync(id);
            if (productInDb is null)
            {
                return NotFound();
            }

            productInDb.Name = updatedProduct.Name;
            productInDb.Price = updatedProduct.Price;
            productInDb.QuantityInStock = updatedProduct.Quantity;

            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product is null)
            {
                return NotFound();
            }

            _db.Remove(product);
            await _db.SaveChangesAsync();
            return Ok();
        }

    }
}