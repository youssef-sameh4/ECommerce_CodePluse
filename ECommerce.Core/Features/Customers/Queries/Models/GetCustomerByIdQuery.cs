using ECommerce.Application.DTOs;
using ECommerce.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Features.Customers.Queries.Models
{
    public class GetCustomerByIdQuery : IRequest<Response<GetCustomerByIdDTO>>
    {
        public int Id { set; get; }

        public GetCustomerByIdQuery(int id)
        {
            Id = id;
        }
    }
}
