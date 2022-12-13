using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneralStore.Data;
using Microsoft.AspNetCore.Mvc;
using GeneralStore.Models;
using Microsoft.EntityFrameworkCore;

namespace GeneralStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private GeneralStoreDbContext _db;
        public CustomerController(GeneralStoreDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromForm]CustomerEdit model)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            Customer customer = new Customer() {
                Name = model.Name,
                Email = model.Email
            };
            _db.Customers.Add(customer);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _db.Customers.ToListAsync();
            return Ok(customers);
        }
    }
}