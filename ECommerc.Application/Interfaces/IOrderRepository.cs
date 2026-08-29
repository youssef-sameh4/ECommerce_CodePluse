using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<Order> GetOrderByIdAsync(int id);
        Task<Order?> GetOrderForCancellationAsync(int id);
        Task<List<Order>> GetCustomerOrdersAsync(int customerId);
    }
}
