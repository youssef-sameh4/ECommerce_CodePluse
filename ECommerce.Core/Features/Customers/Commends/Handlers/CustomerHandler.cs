using AutoMapper;
using ECommerce.Application.Interfaces;
using ECommerce.Core.Bases;
using ECommerce.Core.Features.Customers.Commends.Models;
using ECommerce.Core.Features.Customers.Coomends.Models;
using ECommerce.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Features.Customers.Commends.Handlers
{
    public class CustomerHandler : ResponseFactory, IRequestHandler<CreateCustomerCommend, Response<string>>,
        IRequestHandler<UpgradeToVipCommend, Response<string>>
    {
        private readonly ICustomerServices _customerServices;
        private readonly IMapper _mapper;

        public CustomerHandler(ICustomerServices customerServices, IMapper mapper)
        {
            _customerServices = customerServices;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateCustomerCommend request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.FullName))
                return BadRequest<string>("Full name is required.");

            if (string.IsNullOrWhiteSpace(request.Email) || !request.Email.Contains("@"))
                return BadRequest<string>("A valid email address is required.");
            var customermap = _mapper.Map<Customer>(request);
            var result = await _customerServices.CreateCustomerAsync(customermap);
            if (result == "Email registered")
            {
                return BadRequest<string>("Email is already registered.");
            }
            else if (result == "Success")
            {
                return Created("Customer Created Successfuly");
            }
            else
                return BadRequest<string>();

            }

        public async Task<Response<string>> Handle(UpgradeToVipCommend request, CancellationToken cancellationToken)
        {
            var result = await _customerServices.UpgradeToVipAsync(request.Id);
            if (result == " Customer Null")
            {
                return NotFound<string>("Customer not found.");
            }
            else if (result == "totalSpent less than 500")
            {
                return BadRequest<string>("Customer does not qualify for VIP.");
            }
            else if (result == "Success")
            {
                return Success("Customer Upgrade To Vip Successfuly");
            }
            else
                return BadRequest<string>();
        }
    }
    }

