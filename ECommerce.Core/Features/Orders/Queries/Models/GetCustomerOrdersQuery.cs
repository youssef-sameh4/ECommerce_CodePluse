using ECommerce.Core.Bases;
using ECommerce.Core.Features.Orders.Queries.DTOS;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Features.Orders.Queries.Models
{
    public class GetCustomerOrdersQuery : IRequest<Response<List<OrderResponseDto>>>
    {
        public int customerId{set;get;}

        public GetCustomerOrdersQuery(int customerId)
        {
            this.customerId = customerId;
        }
    }
}
