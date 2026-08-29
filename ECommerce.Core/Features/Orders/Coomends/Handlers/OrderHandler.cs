using AutoMapper;
using ECommerce.Application.DTO.Orders;
using ECommerce.Application.Interfaces;
using ECommerce.Core.Bases;
using ECommerce.Core.Features.Orders.Coomends.Models;
using ECommerce.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Features.Orders.Coomends.Handlers
{
    public class OrderHandler : ResponseFactory, IRequestHandler<CancelOrderCommend, Response<string>>
        , IRequestHandler<CheckoutCommend, Response<string>>
    {
        private readonly IMapper _mapper;
        private readonly IOrderServices _orderServices;

        public OrderHandler(IMapper mapper, IOrderServices orderServices)
        {
            _mapper = mapper;
            _orderServices = orderServices;
        }

        public async Task<Response<string>> Handle(CancelOrderCommend request, CancellationToken cancellationToken)
        {
            var result = await _orderServices.CancelOrderAsync(request.Id);
            if (result == "Null")
            { 
                return NotFound<string>("Order not found."); 
            }
            else if (result == "Order cancelled")
            {
                return BadRequest<string>("Order is already cancelled.");
            }
            else if (result == "Success")
            {
                return Success("Order cancelled Successfuly ");
            }
            else
            {
                return BadRequest<string>();
            }


        }

        public async Task<Response<string>> Handle(CheckoutCommend request, CancellationToken cancellationToken)
        {
            var ordermap = _mapper.Map<CreateOrderDto>(request);
            var result = await _orderServices.CheckoutAsync(ordermap);
            if (result == "empty order")
            {
                return BadRequest<string>("Cannot checkout an empty order.");

            }
            else if (result == "Customer not found")
            {
                return NotFound<string>(
                   $"Customer with ID {request.CustomerId} not found.");
            }
            else if (result == "quantity fail")
            {
                return BadRequest<string>(
                     "Product quantity must be at least 1.");
            }
            else if (result == "Insufficient fail")
            {
                return BadRequest<string>(
                           $"Insufficient stock for product . "
                          );
            }
            else if (result == "Payment processing failed")
            {
                return BadRequest<string>(
                  "Payment processing failed. Amount exceeds limit.");
            }
            else if (result == "coupon null")
            {
                return BadRequest<string>(
                     $"Invalid or inactive coupon code .");
            }
            else if (result == "Success")
            {
                return Success("Order created successfully.");
            }
            else
                return BadRequest<string>();
        }
    }
}
