using DSCC.CW1._9713.API.Models;
using DSCC.CW1._9713.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DSCC.CW1._9713.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CustomerController : ControllerBase
    {
        private readonly IService<Customer> _customerService;

        public CustomerController(IService<Customer> customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAllAsync();
            return Ok(customers);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            await _customerService.DeleteAsync(id);
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _customerService.CreateAsync(customer);
                return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
            }

            return Ok(customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] Customer updatingCustomer)
        {
            if (id != updatingCustomer.Id)
            {
                return BadRequest("User does not exist in the database");
            }

            var existingCustomer = await _customerService.GetByIdAsync(id);

            if (existingCustomer == null)
            {
                return NotFound();
            }

            // Update the properties of the existing customer with the new values.
            existingCustomer.Name = updatingCustomer.Name;
            existingCustomer.Email = updatingCustomer.Email;
            existingCustomer.City = updatingCustomer.City;
            existingCustomer.Country = updatingCustomer.Country;
            existingCustomer.Phone = updatingCustomer.Phone;

            await _customerService.UpdateAsync(existingCustomer);
            return Ok(existingCustomer);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }
    }
}
