using ECommerce.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Features.Customers.Commends.Models
{
    public class UpgradeToVipCommend:IRequest<Response<string>>
    {
        public int Id { set; get; }

        public UpgradeToVipCommend(int id)
        {
            Id = id;
        }
    }
}
