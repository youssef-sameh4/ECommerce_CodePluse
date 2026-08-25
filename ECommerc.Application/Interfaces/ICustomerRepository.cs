using ECommerce.Application.DTOs;
using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Interfaces
{
    public interface ICustomerRepository: IGenericRepository<Customer>
    {
        Task<bool> EmailExists(string Emial);
        Task<Customer?> GetCustomerByIdAsync(int Id);

        Task<decimal> GetTotalSpentByCustomerIdAsync(int customerId);
    }
}
