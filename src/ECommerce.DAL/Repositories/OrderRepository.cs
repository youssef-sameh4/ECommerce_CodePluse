using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.DAL.Context;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrustructur.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly DbSet<Order> _orders ;

        public OrderRepository(AppDbContext context) : base(context)
        {
            _orders = context.Set<Order>();
        }

        public async Task<List<Order>> GetCustomerOrdersAsync(int customerId)
        {
            return await _orders
            .Include(o => o.Items)
            .Where(o => o.CustomerId == customerId)
            .ToListAsync();
        }
        public async Task<Order?> GetOrderForCancellationAsync(int id)
        {
            return await _orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _orders
                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                .Include(o => o.Payment)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}
