using AutoMapper;
using ECommerce.Application.Interfaces;
using ECommerce.Core.Bases;
using ECommerce.Core.Features.Orders.Queries.DTOS;
using ECommerce.Core.Features.Orders.Queries.Models;
using ECommerce.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Features.Orders.Queries.Handlers
{
    public  class OrderHandler: ResponseFactory,IRequestHandler<GetOrderByIdQuery, Response<GetOrderByIdDTO>>
        , IRequestHandler<GetCustomerOrdersQuery, Response<List<OrderResponseDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IOrderServices _orderServices;


        public OrderHandler(IMapper mapper, IOrderServices orderServices)
        {
            _mapper = mapper;
            _orderServices = orderServices;
        }

        public async Task<Response<GetOrderByIdDTO>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _orderServices.GetOrderByIdAsync(request.Id);
            if (product == null)
            {
                return NotFound<GetOrderByIdDTO>(
                         $"Order with ID {request.Id} not found.");
            }
            var response = _mapper.Map<GetOrderByIdDTO>(product);
            return Success(response);

        }

        public async Task<Response<List<OrderResponseDto>>> Handle(GetCustomerOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderServices.GetCustomerOrdersAsync(request.customerId);
            var response = _mapper.Map<List<OrderResponseDto>>(orders);
            return Success(response);

        }
    }
}
