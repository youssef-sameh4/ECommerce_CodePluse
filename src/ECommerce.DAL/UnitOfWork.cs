using ECommerce.Application.Interfaces;
using ECommerce.DAL.Context;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrustructur
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IDbContextTransaction? _transaction;

        public ICustomerRepository Customers { get; }
        public IProductsRepository Products { get; }
        public IOrderRepository Orders { get; }
        public IPaymentRepository Payments { get; }
        public ICouponRepository Coupons { get; }

        public UnitOfWork(
            AppDbContext context,
            ICustomerRepository customers,
            IProductsRepository products,
            IOrderRepository orders,
            IPaymentRepository payments,
            ICouponRepository coupons)
        {
            _context = context;
            Customers = customers;
            Products = products;
            Orders = orders;
            Payments = payments;
            Coupons = coupons;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
            }
        }
    }
}
