using AutoMapper;
using ECommerce.Application.Mappings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ECommerce.Core
{
    public static class ModelCoreDependance
    {
        public static IServiceCollection AddCoreDependance(
            this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(
                    Assembly.GetExecutingAssembly()));

            services.AddAutoMapper(cfg => { }, typeof(CustomerProfile).Assembly);

            return services;
        }
    }
}