using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.DAL.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerServices _customerServices;

    public CustomersController(ICustomerServices customerServices)
    {
        _customerServices = customerServices;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var customer = await _customerServices.GetCustomerByIdAsync(id);

        return StatusCode((int)customer.StatusCode, customer);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateCustomerDto dto)
    {

        var result = await _customerServices.CreateCustomerAsync(dto);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPost("{id}/upgrade-vip")]
    public async Task<IActionResult> UpgradeToVip(int id)
    {
        var result = await _customerServices.UpgradeToVipAsync(id);


        return StatusCode((int)result.StatusCode, result);
    }
}
