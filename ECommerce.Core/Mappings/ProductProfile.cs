using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Core.Features.Products.Coomends.Models;
using ECommerce.Core.Features.Products.Queries.DTOS;
using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Mappings
{

    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductCommend, Product>();
            CreateMap<UpdateProductCommend, Product>();
            CreateMap<Product, GetAllProductsDTO>();
            CreateMap<Product, GetProductByIdDTO>();
        }
    }
}

