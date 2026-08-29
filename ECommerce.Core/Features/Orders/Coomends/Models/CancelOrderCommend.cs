using ECommerce.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Features.Orders.Coomends.Models
{
    public  class CancelOrderCommend:IRequest<Response<string>>
    {
        public int Id { set; get; }

        public CancelOrderCommend(int id)
        {
            Id = id;
        }
    }
}
