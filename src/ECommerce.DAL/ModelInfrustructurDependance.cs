using ECommerce.Application.Interfaces;
using ECommerce.Application.Services;
using ECommerce.Infrustructur.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrustructur
{
    public static class ModelInfrustructurDependance
    {
        public static IServiceCollection AddInfrustrctureDependance(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<ICouponRepository, CouponRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
