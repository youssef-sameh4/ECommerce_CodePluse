using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Mappings
{
    public class OrderProfile: Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderResponseDto>();

            CreateMap<Order, GetOrderByIdDTO>();

            CreateMap<OrderItem, OrderItemResponseDto>();

            CreateMap<Payment, PaymentResponseDto>();
        }
    }
}
