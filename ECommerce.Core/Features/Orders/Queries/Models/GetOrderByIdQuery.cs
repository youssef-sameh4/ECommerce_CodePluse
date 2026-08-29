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
    public class GetOrderByIdQuery:IRequest<Response<GetOrderByIdDTO>>
    {
        public int Id { set; get; }

        public GetOrderByIdQuery(int id)
        {
            Id = id;
        }
    }
}
