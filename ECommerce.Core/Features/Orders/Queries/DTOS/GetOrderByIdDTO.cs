using ECommerce.Core.Features.Orders.Queries.DTOs;
using ECommerce.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Features.Orders.Queries.DTOS
{
    public class GetOrderByIdDTO
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public DateTime CreatedAt { get; set; }

        public OrderStatus Status { get; set; }

        public decimal Subtotal { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal ShippingFee { get; set; }

        public decimal TotalAmount { get; set; }

        public List<OrderItemResponseDto> Items { get; set; } = new();

        public PaymentResponseDto? Payment { get; set; }
    }
}
