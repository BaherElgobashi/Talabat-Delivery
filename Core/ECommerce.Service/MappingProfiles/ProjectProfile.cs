using AutoMapper;
using ECommerce.Domain.Models.Baskets;
using ECommerce.Domain.Models.Identity;
using ECommerce.Domain.Models.Orders;
using ECommerce.Domain.Models.Products;
using ECommerce.Shared.Dtos.Basket_Dtos;
using ECommerce.Shared.Dtos.IdentityDtos;
using ECommerce.Shared.Dtos.OrderDtos;
using ECommerce.Shared.Dtos.ProductDtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.MappingProfiles
{
    public class ProjectProfile : Profile
    {
        
        public ProjectProfile(IConfiguration configuration)
        {
            
            CreateMap<Product, ProductDto>()
                .ForMember(dist => dist.BrandName, options => options.MapFrom(src => src.Brand.Name))
                .ForMember(dist => dist.TypeName, options => options.MapFrom(src => src.Type.Name))
                .ForMember(dist => dist.PictureUrl, options => options.MapFrom(new PictureUrlResolver(configuration)));

            CreateMap<ProductBrand, BrandDto>();

            CreateMap<ProductType , TypeDto>();

            CreateMap<CustomerBasket , BasketDto>().ReverseMap();
            CreateMap<BasketItem , BasketItemDto>().ReverseMap();

            CreateMap<Address , AddressDto>().ReverseMap();

            #region Order.

            CreateMap<AddressDto , OrderAddress>().ReverseMap();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(D => D.DeliveryMethod, options => options.MapFrom(src => src.DeliveryMethod.ShortName))
                .ForMember(D => D.OrderState, options => options.MapFrom(src => src.OrderStatus));

            CreateMap<OrderItem , OrderItemDto>()
                .ForMember(src => src.ProductName , options => options.MapFrom(src => src.Product.ProductName))
                .ForMember(src => src.ProductPictureUrl , options => options.MapFrom(new OrderPictureResolver(configuration)));

            CreateMap<DeliveryMethod, DeliveryMethodDto>().ReverseMap();



            #endregion
        }
    }
}
