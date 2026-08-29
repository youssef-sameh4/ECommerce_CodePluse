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
       
        Task<Customer?> GetCustomerByIdAsync(int Id);
        Task<string> CreateCustomerAsync(Customer customer);
        Task<string> UpgradeToVipAsync(int customerId);

    }
}
