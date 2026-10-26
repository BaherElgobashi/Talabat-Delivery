using AutoMapper;
using AutoMapper.Execution;
using ECommerce.Domain.Models.Orders;
using ECommerce.Shared.Dtos.OrderDtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.MappingProfiles
{
    internal class OrderPictureResolver(IConfiguration configuration) : IValueResolver<OrderItem, OrderItemDto, string>
    {
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if(string.IsNullOrEmpty(source.Product.ProductUrl)) return string.Empty;

            else
            {
                var Url = $"{configuration.GetSection("Urls")["BaseUrl"]}{source.Product.ProductUrl}";

                return Url;
            }
        }
    }
}
