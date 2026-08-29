using AutoMapper;
using ECommerce.Application.Interfaces;
using ECommerce.Core.Bases;
using ECommerce.Core.Features.Products.Coomends.Models;
using ECommerce.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Features.Products.Coomends.Handlers
{
    public class ProdectHandler : ResponseFactory, IRequestHandler<CreateProductCommend, Response<string>>,
        IRequestHandler<DeleteProductCommend, Response<string>>,
         IRequestHandler<UpdateProductCommend, Response<string>>
    {
        private readonly IMapper _mapper;
        private readonly IProductServices _productServices;

        public ProdectHandler(IMapper mapper, IProductServices productServices)
        {
            _mapper = mapper;
            _productServices = productServices;
        }

        public async  Task<Response<string>> Handle(CreateProductCommend request, CancellationToken cancellationToken)
        {
            var productmap = _mapper.Map<Product>(request);
            var result = await _productServices.CreateProductAsync(productmap);
            if (result== "Product price fail")
            {
                return BadRequest<string>("Product price must be greater than zero.");
            }

            else if (result == "Stock quantity fail")
            {
                return BadRequest<string>("Stock quantity cannot be negative.");
            }
            else if(result == "SKU fail.")
            {
                return BadRequest<string>("");
            }
            else if (result == "Success")
            {
                return Created("Product Created Successfuly");
            }
            else
            {
                return BadRequest<string>();
            }

        }

        public async Task<Response<string>> Handle(DeleteProductCommend request, CancellationToken cancellationToken)
        {
            var result = await _productServices.DeletProductAsync(request.Id);
            if (result == "Null")
            {
                return NotFound<string>($"Product with ID {request.Id} not found.");

            }
            else if (result == "Success")
            {
                return Created("Product Deleted Successfuly");
            }
            else
            {
                return BadRequest<string>();
            }
        }
        public async Task<Response<string>> Handle(UpdateProductCommend request, CancellationToken cancellationToken)
        {
            var productmap = _mapper.Map<Product>(request);
            var result = await _productServices.UpdateProductAsync(productmap);
            if (result == "Null")
            {
                return NotFound<string>($"Product with ID {request.Id} not found.");

            }
            else if (result == "Price Fail")
            {

                return BadRequest<string>("Price must be positive.");
            }
            else if (result == "Success")
            {
                return Created("Product Updated Successfuly");
            }
            else
            {
                return BadRequest<string>();
            }

        }
    }
}
