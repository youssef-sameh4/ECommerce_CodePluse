using ECommerce.Application.Bases;
using ECommerce.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Interfaces
{
    public interface IOrderServices
    {
        Task<Response<string>> CancelOrderAsync(int id);
        Task<Response<GetOrderByIdDTO>> GetOrderByIdAsync(int id);
        Task<Response<string>> CheckoutAsync(CreateOrderDto dto);
        Task<Response<List<OrderResponseDto>>> GetCustomerOrdersAsync(int customerId);
    }
}
