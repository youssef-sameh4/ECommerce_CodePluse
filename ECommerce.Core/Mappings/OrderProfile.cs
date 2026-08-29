using AutoMapper;
using ECommerce.Application.DTO.Orders;
using ECommerce.Core.Features.Orders.Coomends.Models;
using ECommerce.Core.Features.Orders.Queries.DTOs;
using ECommerce.Core.Features.Orders.Queries.DTOS;
using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Mappings
{
    
        public class OrderProfile : Profile
        {
            public OrderProfile()
            {
                CreateMap<CheckoutCommend, CreateOrderDto>();
                CreateMap<Order, OrderResponseDto>();
                CreateMap<Order, GetOrderByIdDTO>();
                CreateMap<OrderItem, OrderItemResponseDto>();
                CreateMap<Payment, PaymentResponseDto>();
            }
        }
    
}
