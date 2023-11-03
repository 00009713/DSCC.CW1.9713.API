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
        public IActionResult GetAllOrders()
        {
            var orders = _orderService.GetAll();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            var order = _orderService.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public IActionResult CreateOrder(Order order)
        {
            if (ModelState.IsValid)
            {
                _orderService.Create(order);
                return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, Order updatedOrder)
        {
            if (id != updatedOrder.Id)
            {
                return BadRequest("Invalid request");
            }

            var existingOrder = _orderService.GetById(id);

            if (existingOrder == null)
            {
                return NotFound();
            }

            // Update the properties of the existing order with the new values.
            existingOrder.Amount = updatedOrder.Amount;
            existingOrder.Name = updatedOrder.Name;
            existingOrder.TotalPrice = updatedOrder.TotalPrice;

            _orderService.Update(existingOrder);
            return Ok(existingOrder);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var order = _orderService.GetById(id);
            if (order == null)
            {
                return NotFound();
            }

            _orderService.Delete(id);
            return NoContent();
        }
    }
}
