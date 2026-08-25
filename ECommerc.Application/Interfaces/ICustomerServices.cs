using ECommerce.Application.Bases;
using ECommerce.Application.DTOs;
using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Interfaces
{
    public interface ICustomerServices
    {
       
        Task<Response<GetCustomerByIdDTO>> GetCustomerByIdAsync(int Id);
        Task<Response<string>> CreateCustomerAsync(CreateCustomerDto dto);
        Task<Response<string>> UpgradeToVipAsync(int customerId);

    }
}
