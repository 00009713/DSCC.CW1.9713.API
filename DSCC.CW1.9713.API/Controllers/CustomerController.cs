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
        public IActionResult GetAllCustomers()
        {
            return Ok(_customerService.GetAll());
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = _customerService.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }

            _customerService.Delete(id);
            return Ok(customer);
        }

        [HttpPost]
        public IActionResult CreateCustomer(Customer customer) 
        {
            if (ModelState.IsValid)
            {
                _customerService.Create(customer);
                return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
            }

            return Ok(customer);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, Customer updatingCustomer) 
        {
            if (id != updatingCustomer.Id)
            {
                return BadRequest("User is not exists in database");
            }

            var existingOrder = _customerService.GetById(id);

            if (existingOrder == null)
            {
                return NotFound();
            }

            // Update the properties of the existing order with the new values.
            existingOrder.Name = updatingCustomer.Name;
            existingOrder.Email = updatingCustomer.Email;
            existingOrder.City = updatingCustomer.City;
            existingOrder.Country = updatingCustomer.Country;
            existingOrder.Phone = updatingCustomer.Phone;

            _customerService.Update(existingOrder);
            return Ok(existingOrder);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            var customer = _customerService.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }
    }
}
