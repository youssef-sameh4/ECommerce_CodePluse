using ECommerce.Application.DTO.Orders;
using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Interfaces
{
    public interface IOrderServices
    {
        Task<string> CancelOrderAsync(int id);
        Task<Order> GetOrderByIdAsync(int id);
        Task<string> CheckoutAsync(CreateOrderDto dto);
        Task<List<Order>> GetCustomerOrdersAsync(int customerId);
    }
}
