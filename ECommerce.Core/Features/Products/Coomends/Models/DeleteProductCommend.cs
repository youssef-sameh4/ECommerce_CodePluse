using ECommerce.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Features.Products.Coomends.Models
{
    public class DeleteProductCommend:IRequest<Response<string>>
    {
        public int Id { set; get; }

        public DeleteProductCommend(int id)
        {
            Id = id;
        }
    }
}
