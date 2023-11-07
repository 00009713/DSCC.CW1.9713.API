using DSCC.CW1._9713.API.Models;
using DSCC.CW1._9713.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DSCC.CW1._9713.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrderController : ControllerBase
    {
        private readonly IService<Order> _orderService;

        public OrderController(IService<Order> orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            if (ModelState.IsValid)
            {
                await _orderService.CreateAsync(order);
                return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order updatedOrder)
        {
            if (id != updatedOrder.Id)
            {
                return BadRequest("Invalid request");
            }

            var existingOrder = await _orderService.GetByIdAsync(id);

            if (existingOrder == null)
            {
                return NotFound();
            }

            // Update the properties of the existing order with the new values.
            existingOrder.Amount = updatedOrder.Amount;
            existingOrder.Name = updatedOrder.Name;
            existingOrder.TotalPrice = updatedOrder.TotalPrice;

            await _orderService.UpdateAsync(existingOrder);
            return Ok(existingOrder);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            await _orderService.DeleteAsync(id);
            return NoContent();
        }
    }
}
