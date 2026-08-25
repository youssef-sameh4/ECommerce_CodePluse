using AutoMapper;
using ECommerce.Application.Bases;
using ECommerce.Application.DTOs;
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
    public class OrderServices : ResponseFactory, IOrderServices
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

        public async Task<Response<GetOrderByIdDTO>> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);

            if (order == null)
                return NotFound<GetOrderByIdDTO>(
                    $"Order with ID {id} not found.");

            var response = _mapper.Map<GetOrderByIdDTO>(order);

            return Success(response);
        }

        public async Task<Response<List<OrderResponseDto>>> GetCustomerOrdersAsync(int customerId)
        {
            var orders = await _orderRepository.GetCustomerOrdersAsync(customerId);

            var response = _mapper.Map<List<OrderResponseDto>>(orders);

            return Success(response);
        }
        public async Task<Response<string>> CancelOrderAsync(int id)
        {
            var order = await _orderRepository.GetOrderForCancellationAsync(id);

            if (order == null)
                return NotFound<string>("Order not found.");

            if (order.Status == OrderStatus.Cancelled)
                return BadRequest<string>("Order is already cancelled.");

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

            return Success("Order cancelled successfully.");
        }

        public async Task<Response<string>> CheckoutAsync(CreateOrderDto dto)
        {
            // 1. Validate Order Items
            if (dto.Items == null || !dto.Items.Any())
            {
                return BadRequest<string>("Cannot checkout an empty order.");
            }

            // 2. Get Customer
            var customer = await _unitOfWork.Customers.GetByIdAsync(dto.CustomerId);

            if (customer == null)
            {
                return NotFound<string>(
                    $"Customer with ID {dto.CustomerId} not found.");
            }

            decimal subtotal = 0m;

            var orderItems = new List<OrderItem>();

            // 3. Validate Products & Stock
            foreach (var itemDto in dto.Items)
            {
                if (itemDto.Quantity <= 0)
                {
                    return BadRequest<string>(
                        "Product quantity must be at least 1.");
                }

                var product = await _unitOfWork.Products
                    .GetByIdAsync(itemDto.ProductId);

                if (product == null)
                {
                    return NotFound<string>(
                        $"Product with ID {itemDto.ProductId} not found.");
                }

                if (product.StockQuantity < itemDto.Quantity)
                {
                    return BadRequest<string>(
                        $"Insufficient stock for product '{product.Name}'. " +
                        $"Available: {product.StockQuantity}, " +
                        $"Requested: {itemDto.Quantity}");
                }

                // Calculate subtotal
                subtotal += product.Price * itemDto.Quantity;

                // Create OrderItem
                orderItems.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = itemDto.Quantity,
                    UnitPrice = product.Price
                });

                // Decrease stock
                product.StockQuantity -= itemDto.Quantity;
            }

            // 4. Calculate Discount
            decimal discount = 0m;

            // VIP Discount
            if (customer.IsVip)
            {
                discount += Math.Round(subtotal * 0.15m, 2);
            }

            // Coupon Discount
            if (!string.IsNullOrWhiteSpace(dto.CouponCode))
            {
                var coupon = await _unitOfWork.Coupons
                    .GetActiveCouponByCodeAsync(dto.CouponCode);

                if (coupon == null)
                {
                    return BadRequest<string>(
                        $"Invalid or inactive coupon code '{dto.CouponCode}'.");
                }

                discount += Math.Round(
                    subtotal * (coupon.DiscountPercentage / 100m),
                    2);
            }

            // Discount cannot exceed subtotal
            if (discount > subtotal)
            {
                discount = subtotal;
            }

            // 5. Calculate Amounts
            var netAmount = subtotal - discount;

            var tax = Math.Round(netAmount * 0.14m, 2);

            var shipping = netAmount >= 1000m
                ? 0m
                : 75m;

            var finalTotal = netAmount + tax + shipping;

            // 6. Check Payment Limit
            if (finalTotal > 50000m)
            {
                return BadRequest<string>(
                    "Payment processing failed. Amount exceeds limit.");
            }

            // 7. Generate Transaction Reference
            var txRef =
                $"TX-LEGACY-{Guid.NewGuid().ToString()[..8].ToUpper()}";

            // 8. Create Order
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

            // 9. Create Payment
            var payment = new Payment
            {
                Order = order,
                Amount = finalTotal,
                PaymentDate = DateTime.UtcNow,
                TransactionReference = txRef,
                IsSuccess = true
            };

            // 10. Transaction
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

                return InternalServerError<string>(
                    "An error occurred while saving the order.");
            }

            // 11. Response
            return Success(
                $"Order created successfully. " +
                $"Order ID: {order.Id}, " +
                $"Total: {order.TotalAmount}, " +
                $"Transaction Reference: {txRef}");
        }
    }
}
