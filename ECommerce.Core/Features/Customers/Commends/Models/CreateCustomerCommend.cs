using ECommerce.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Features.Customers.Coomends.Models
{
    public class CreateCustomerCommend:IRequest<Response<string>>
    {
        public CreateCustomerCommend(string fullName, string email, bool isVip)
        {
            FullName = fullName;
            Email = email;
            IsVip = isVip;
        }

        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsVip { get; set; }
    }

}

