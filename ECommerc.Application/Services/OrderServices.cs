using AutoMapper;
using ECommerce.Application.DTO.Orders;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Services
{
    public class OrderServices :  IOrderServices
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IProductsRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        public OrderServices(
            IOrderRepository orderRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);

            if (order == null)
            { 
                return null; 
            }


            return order;
        }

        public async Task<List<Order>> GetCustomerOrdersAsync(int customerId)
        {
            var orders = await _orderRepository.GetCustomerOrdersAsync(customerId);

           
            return orders;
        }
        public async Task<string> CancelOrderAsync(int id)
        {
            var order = await _orderRepository.GetOrderForCancellationAsync(id);

            if (order == null)
                return "Order Null";

            if (order.Status == OrderStatus.Cancelled)
                return "Order cancelled";

            if (order.Status == OrderStatus.Paid)
            {
                foreach (var item in order.Items)
                {
                    var product = await _productRepository.GetByIdAsync(item.ProductId);

                    if (product != null)
                    {
                        product.StockQuantity += item.Quantity;
                    }
                }
            }

            order.Status = OrderStatus.Cancelled;

            await _orderRepository.SaveChangesAsync();

            return "Success";
        }

        public async Task<string> CheckoutAsync(CreateOrderDto dto)
        {
            if (dto.Items == null || !dto.Items.Any())
            {
                return "empty order";
            }

            var customer = await _unitOfWork.Customers.GetByIdAsync(dto.CustomerId);

            if (customer == null)
            { 
                return 
                    $"Customer not found";
            }

            decimal subtotal = 0m;

            var orderItems = new List<OrderItem>();

            foreach (var itemDto in dto.Items)
            {
                if (itemDto.Quantity <= 0)
                {
                    return "quantity fail";
                }

                var product = await _unitOfWork.Products
                    .GetByIdAsync(itemDto.ProductId);

                if (product == null)
                {
                    return "product not found";
                }

                if (product.StockQuantity < itemDto.Quantity)
                {
                    return "Insufficient fail";
                }

                subtotal += product.Price * itemDto.Quantity;

                orderItems.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = itemDto.Quantity,
                    UnitPrice = product.Price
                });

                product.StockQuantity -= itemDto.Quantity;
            }

            decimal discount = 0m;

            if (customer.IsVip)
            {
                discount += Math.Round(subtotal * 0.15m, 2);
            }

            if (!string.IsNullOrWhiteSpace(dto.CouponCode))
            {
                var coupon = await _unitOfWork.Coupons
                    .GetActiveCouponByCodeAsync(dto.CouponCode);

                if (coupon == null)
                {
                    return "coupon null";
                 
                }

                discount += Math.Round(
                    subtotal * (coupon.DiscountPercentage / 100m),
                    2);
            }

            if (discount > subtotal)
            {
                discount = subtotal;
            }

            var netAmount = subtotal - discount;

            var tax = Math.Round(netAmount * 0.14m, 2);

            var shipping = netAmount >= 1000m
                ? 0m
                : 75m;

            var finalTotal = netAmount + tax + shipping;

            if (finalTotal > 50000m)
            {
                return "Payment processing failed";
              
            }

            var txRef =
                $"TX-LEGACY-{Guid.NewGuid().ToString()[..8].ToUpper()}";

            var order = new Order
            {
                CustomerId = customer.Id,
                CreatedAt = DateTime.UtcNow,
                Status = OrderStatus.Paid,

                Subtotal = subtotal,
                DiscountAmount = discount,
                TaxAmount = tax,
                ShippingFee = shipping,
                TotalAmount = finalTotal,

                Items = orderItems
            };

            var payment = new Payment
            {
                Order = order,
                Amount = finalTotal,
                PaymentDate = DateTime.UtcNow,
                TransactionReference = txRef,
                IsSuccess = true
            };

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                await _unitOfWork.Orders.AddAsync(order);

                await _unitOfWork.Payments.AddAsync(payment);

                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();

            }

            return
                $"Success";
           
        }
    }
}
