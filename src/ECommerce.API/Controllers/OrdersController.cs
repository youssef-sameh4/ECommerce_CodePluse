using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderServices _orderServices;

        public OrdersController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOrder(int id)
        {
            var result = await _orderServices.GetOrderByIdAsync(id);

            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult> GetCustomerOrders(int customerId)
        {
            var result = await _orderServices.GetCustomerOrdersAsync(customerId);

            return StatusCode((int)result.StatusCode, result);
        }
        [HttpPost("cancel/{id}")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            var result = await _orderServices.CancelOrderAsync(id);

            return StatusCode((int)result.StatusCode, result);
        }
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CreateOrderDto dto)
        {
            var result = await _orderServices.CheckoutAsync(dto);

            return StatusCode((int)result.StatusCode, result);
        }
    }
}
