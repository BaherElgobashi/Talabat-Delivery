using AutoMapper;
using ECommerce.Domain.Entities.Products;
using ECommerce.Shared.DTOS.ProductDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductBrand, BrandDTO>();
        }
    }
}
