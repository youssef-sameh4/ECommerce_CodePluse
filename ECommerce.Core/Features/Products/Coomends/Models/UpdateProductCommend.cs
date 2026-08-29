using ECommerce.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Features.Products.Coomends.Models
{
    public class UpdateProductCommend:IRequest<Response<string>>
    {
        public UpdateProductCommend(int id,string name, string sKU, decimal price, int stockQuantity)
        {
            Id = id;
            Name = name;
            SKU = sKU;
            Price = price;
            StockQuantity = stockQuantity;
        }
        public int Id { set; get; }
        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string SKU { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int StockQuantity { get; set; }
    }
}
