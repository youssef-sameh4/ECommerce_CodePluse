using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Core.Features.Customers.Coomends.Models;
using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Mappings
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CreateCustomerCommend, Customer>();
            CreateMap<Customer, GetCustomerByIdDTO>();
            CreateMap<Order, OrderResponseDto>();
        }
    }
}
