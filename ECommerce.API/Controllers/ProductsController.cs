using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Core.Features.Products.Coomends.Models;
using ECommerce.Core.Features.Products.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(
            [FromBody] CreateProductCommend request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(
            int id,
            [FromBody] UpdateProductCommend request)
        {
            request.Id = id;

            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var request = new DeleteProductCommend(id);

            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var request = new GetAllProductsQuery();

            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var request = new GetByIdProductQuery(id);

            var response = await _mediator.Send(request);

            return Ok(response);
        }
    }
}