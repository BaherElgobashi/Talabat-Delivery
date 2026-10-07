using AutoMapper;
using ECommerce.Domain.Models.Products;
using ECommerce.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.MappingProfiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dist => dist.BrandName, options => options.MapFrom(src => src.Brand.Name))
                .ForMember(dist => dist.TypeName, options => options.MapFrom(src => src.Type.Name));

            CreateMap<ProductBrand, BrandDto>();

            CreateMap<ProductType , TypeDto>();
        }
    }
}
