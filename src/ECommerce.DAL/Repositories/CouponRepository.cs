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
    public class CouponRepository:GenericRepository<Coupon>, ICouponRepository
    {
        private readonly DbSet<Coupon> _coupons;

        public CouponRepository(AppDbContext context)
            : base(context)
        {
            _coupons = context.Set<Coupon>();
        }

        public async Task<Coupon?> GetActiveCouponByCodeAsync(string code)
        {
            return await _coupons
                .FirstOrDefaultAsync(c =>
                    c.Code.ToLower() == code.ToLower() &&
                    c.IsActive);
        }
    }
}
