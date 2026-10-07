using AutoMapper;
using ECommerce.Domain.Models.Products;
using ECommerce.Shared.Dtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.MappingProfiles
{
    public class PictureUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration configuration;

        public PictureUrlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // Dynamic Resolve.
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
                return string.Empty;

            else
            {
                var Url = $"{configuration.GetSection("Urls")["BaseUrl"]}{source.PictureUrl}";

                return Url;
            }
        }

        #region Static Resolve.
        // Static Resolve.
        //public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        //{
        //    if(string.IsNullOrEmpty(source.PictureUrl))
        //        return string.Empty;

        //    else
        //    {
        //        var Url = $"https://localhost:7119/{source.PictureUrl}";

        //        return Url;
        //    }
        //} 
        #endregion
    }
}
