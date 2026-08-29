using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Core.Features.Orders.Coomends.Models;
using ECommerce.Core.Features.Orders.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout(
            [FromBody] CheckoutCommend request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            var request = new CancelOrderCommend(id);

            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var request = new GetOrderByIdQuery(id);

            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetCustomerOrders(int customerId)
        {
            var request = new GetCustomerOrdersQuery(customerId);

            var response = await _mediator.Send(request);

            return Ok(response);
        }
    }
}
