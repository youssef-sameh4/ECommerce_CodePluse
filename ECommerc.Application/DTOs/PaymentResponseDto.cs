using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.DTOs
{
    public class PaymentResponseDto
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public string TransactionReference { get; set; } = string.Empty;

        public bool IsSuccess { get; set; }
    }
}
