using ECommerce.Core.Bases;
using ECommerce.Core.Features.Products.Queries.DTOS;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Features.Products.Queries.Models
{
    public class GetAllProductsQuery:IRequest<Response<List<GetAllProductsDTO>>>
    {
    }
}
