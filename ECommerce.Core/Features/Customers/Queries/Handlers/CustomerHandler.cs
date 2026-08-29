using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Services;
using ECommerce.Core.Bases;
using ECommerce.Core.Features.Customers.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Features.Customers.Queries.Handlers
{
    public class CustomerHandler : ResponseFactory, IRequestHandler<GetCustomerByIdQuery, Response<GetCustomerByIdDTO>>
    {
        private readonly ICustomerServices _customerServices;
        private readonly IMapper _mapper ;

        public CustomerHandler(ICustomerServices customerServices, IMapper mapper)
        {
            _customerServices = customerServices;
            _mapper = mapper;
        }

        public async Task<Response<GetCustomerByIdDTO>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerServices.GetCustomerByIdAsync(request.Id);
            if (customer == null)
            {
                return NotFound<GetCustomerByIdDTO>("Customer Not Found");
            }
            var customermap = _mapper.Map<GetCustomerByIdDTO>(customer);
            return Success(customermap);
        }
    }
}
