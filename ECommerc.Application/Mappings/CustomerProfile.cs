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
    public class CustomerProfile: Profile
    {
        public CustomerProfile()
        {
            CreateMap<CreateCustomerDto, Customer>();

            CreateMap<Customer, GetCustomerByIdDTO>();
            CreateMap<Order, OrderResponseDto>();
        }
    }
}
