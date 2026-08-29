using AutoMapper;
using ECommerce.Application.Interfaces;
using ECommerce.Core.Bases;
using ECommerce.Core.Features.Products.Queries.DTOS;
using ECommerce.Core.Features.Products.Queries.Models;
using ECommerce.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Features.Products.Queries.Handlers
{
    public class ProductHandler : ResponseFactory, IRequestHandler<GetAllProductsQuery, Response<List<GetAllProductsDTO>>>,
        IRequestHandler<GetByIdProductQuery, Response<GetProductByIdDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IProductServices _productServices;

        public ProductHandler(IMapper mapper, IProductServices productServices)
        {
            _mapper = mapper;
            _productServices = productServices;
        }

        public async Task<Response<List<GetAllProductsDTO>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productServices.GetAllProductAsync();
           var response= _mapper.Map<List<GetAllProductsDTO>>(products);
            return Success(response);
        }

        public async Task<Response<GetProductByIdDTO>> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _productServices.GetProductByIdAsync(request.Id);
            if (product == null)
            {
                return NotFound<GetProductByIdDTO>($"Product with ID {request.Id} not found.");

            }
            var response = _mapper.Map<GetProductByIdDTO>(product);
            return Success(response);

        }
    }
}
