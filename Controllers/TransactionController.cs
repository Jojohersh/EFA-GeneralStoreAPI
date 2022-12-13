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
    public class TransactionController : ControllerBase
    {
        private GeneralStoreDbContext _db;
        public TransactionController(GeneralStoreDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromForm] TransactionEdit model)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var transaction = new Transaction() {
                ProductId = model.ProductId,
                CustomerId = model.CustomerId,
                Quantity = model.Quantity,
                
                DateOfTransaction = DateTime.Now
            };
            // an attempt at changing the quantity of the product being purchased
            //todo: make this actually work
            var product = await _db.Products.FindAsync(transaction.ProductId);
            product.QuantityInStock = product.QuantityInStock - transaction.Quantity;

            _db.Transactions.Add(transaction);
            await _db.SaveChangesAsync();
            return Ok();
        }
        //todo: figure out how to include foreign info
        [HttpGet]
        public async Task<IActionResult> GetAllTransactions()
        {
            var transactions = await _db.Transactions
            //todo - keep getting object cycle error
            // .Include(transaction => transaction.Customer.Name)
            // .Include(transaction => transaction.Customer.Email)
            // .Include(transaction => transaction.Product)
            .ToListAsync();
            return Ok(transactions);
        }
    }
}