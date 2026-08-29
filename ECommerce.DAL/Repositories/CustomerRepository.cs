using ECommerce.Application.Interfaces;
using ECommerce.DAL.Context;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrustructur.Repositories
{
    public class CustomerRepository: GenericRepository<Customer>, ICustomerRepository
    {
        private readonly DbSet<Customer> _customers;

        public CustomerRepository(AppDbContext context) : base(context)
        {
            _customers = context.Set<Customer>();
        }

        public async Task<bool> EmailExists(string email)
        {
            return await _customers.AnyAsync(c => c.Email.ToLower() == email.ToLower());
        }

        public async Task<Customer?> GetCustomerByIdAsync(int Id)
        {
            return await _customers
             .Include(c => c.Orders)
             .FirstOrDefaultAsync(c => c.Id == Id);

        }

        public async Task<decimal> GetTotalSpentByCustomerIdAsync(int customerId)
        {
          return await _customers
        .Where(c => c.Id == customerId)
        .SelectMany(c => c.Orders)
        .Where(o => o.Status == OrderStatus.Paid)
        .SumAsync(o => o.TotalAmount);
        }
    }
}
