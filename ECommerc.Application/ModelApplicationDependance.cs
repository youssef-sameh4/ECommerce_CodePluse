using ECommerce.Application.Interfaces;
using ECommerce.Application.Mappings;
using ECommerce.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application
{
    public static class ModelApplicationDependance
    {
        public static IServiceCollection AddApplicationDependance(this IServiceCollection services)
        {
            services.AddTransient<ICustomerServices, CustomerServices>();
            services.AddTransient<IProductServices, ProductServices>();
            services.AddTransient<IOrderServices, OrderServices>();

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<CustomerProfile>();
            });
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<ProductProfile>();
            });
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<OrderProfile>();
            });
            return services;
        }
    }
}
