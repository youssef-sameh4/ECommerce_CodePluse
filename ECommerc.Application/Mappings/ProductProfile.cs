using AutoMapper;
using ECommerce.Application.DTOs;
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
                CreateMap<CreateProductDto, Product>();

                CreateMap<Product, GetAllProductsDTO>();

                CreateMap<Product, GetProductByIdDTO>();

                CreateMap<UpdateProductDTO, Product>();
            }
        }
    }

