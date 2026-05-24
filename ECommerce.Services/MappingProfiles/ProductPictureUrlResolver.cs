using AutoMapper;
using ECommerce.Domain.Entities.Products;
using ECommerce.Shared.DTOS.ProductDTOS;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.MappingProfiles
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductDTO, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
                return string.Empty;

            if (source.PictureUrl.StartsWith("http"))
                return source.PictureUrl;

            var BaseUrl = _configuration.GetSection("URLs")["BaseUrl"];

            if(string.IsNullOrEmpty(BaseUrl))
                return string.Empty;

            var picUrl = $"{BaseUrl}{source.PictureUrl}";

            return picUrl;


        }
    }
}
