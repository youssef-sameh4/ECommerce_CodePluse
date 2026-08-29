using ECommerce.Application.DTO.Orders;
using ECommerce.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Features.Orders.Coomends.Models
{
    public class CheckoutCommend:IRequest<Response<string>>
    {
        public CheckoutCommend(int customerId, List<OrderItemRequestDto> items, string? couponCode)
        {
            CustomerId = customerId;
            Items = items;
            CouponCode = couponCode;
        }

        public int CustomerId { get; set; }
        public List<OrderItemRequestDto> Items { get; set; } = new();
        public string? CouponCode { get; set; }
    }
}
