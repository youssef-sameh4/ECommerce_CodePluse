using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Core.Features.Customers.Commends.Models;
using ECommerce.Core.Features.Customers.Coomends.Models;
using ECommerce.Core.Features.Customers.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer(
        [FromBody] CreateCustomerCommend request)
    {
        var response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut("{id}/upgrade-vip")]
    public async Task<IActionResult> UpgradeToVip(int id)
    {
        var request = new UpgradeToVipCommend(id);

        var response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerById(int id)
    {
        var request = new GetCustomerByIdQuery(id);

        var response = await _mediator.Send(request);

        return Ok(response);
    }
}