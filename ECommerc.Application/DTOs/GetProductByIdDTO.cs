using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.DTOs
{
    public class GetProductByIdDTO
    {
        public int Id { get; set; }


        public string Name { get; set; } = string.Empty;

        public string SKU { get; set; } = string.Empty;


        public decimal Price { get; set; }

        public int StockQuantity { get; set; }
    }
}
