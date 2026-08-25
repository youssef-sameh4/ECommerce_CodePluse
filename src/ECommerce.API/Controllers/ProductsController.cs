using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductsController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var result = await _productServices.GetAllProductAsync();

            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _productServices.GetProductByIdAsync(id);

            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateProductDto dto)
        {
            var result = await _productServices.CreateProductAsync(dto);

            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(
            int id,
            [FromBody] UpdateProductDTO dto)
        {
            var result = await _productServices.UpdateProductAsync(id, dto);

            return StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _productServices.DeletProductAsync(id);

            return StatusCode((int)result.StatusCode, result);
        }
    }
}

