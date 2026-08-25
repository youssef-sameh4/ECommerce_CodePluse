using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Interfaces
{
    public interface IUnitOfWork
    {
        ICustomerRepository Customers { get; }
        IProductsRepository Products { get; }
        IOrderRepository Orders { get; }
        IPaymentRepository Payments { get; }
        ICouponRepository Coupons { get; }

        Task<int> SaveChangesAsync();

        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
