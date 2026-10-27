using AutoMapper;
using ECommerce.Abstraction.IServices;
using ECommerce.Domain.Contratcs.Repos;
using ECommerce.Domain.Contratcs.UOW;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.Models.Orders;
using ECommerce.Domain.Models.Products;
using ECommerce.Shared.Dtos.IdentityDtos;
using ECommerce.Shared.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Services
{
    public class OrderService(IMapper mapper , IBasketRepository  basketRepository , IUnitOfWork unitOfWork) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto, string Email)
        {
            // 1.Map Address To Order Address.
            var OrderAddress = mapper.Map<AddressDto , OrderAddress>(orderDto.Address);

            // 2.Get Basket.
            var Basket = await basketRepository.GetBasketAsync(orderDto.BasketId) ?? throw new BasketNotFoundException(orderDto.BasketId);

            // 3.Create OrderItems List.
            List<OrderItem> orderItems = [];

            var ProductRepo = unitOfWork.GetRepository<Product , int>();
            foreach (var Item in Basket.Items)
            {
                var Product = await ProductRepo.GetByIdAsync(Item.Id) ?? throw new ProductNotFound(Item.Id);

                var OrderItem = new OrderItem()
                {
                    Product = new ProductItemOrdered()
                    {
                        ProductId = Product.Id,
                        ProductName = Product.Name,
                        ProductUrl = Product.PictureUrl

                    },
                    Quantity = Item.Quantity,
                    Price = Product.Price
                };
                orderItems.Add(OrderItem);

            }

            // 4.Get Delivery Method.
            var DeliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId)
                ?? throw new DeliveryMethodNotFoundException(orderDto.DeliveryMethodId);

            // 5.SubTotal.
            var SubTotal = orderItems.Sum(I => I.Quantity * I.Price);

            // Prepare The Whole Order using order constructor.
            var Order = new Order(Email , OrderAddress , DeliveryMethod , orderItems , SubTotal);

            // Add Order inside the Table.
            unitOfWork.GetRepository<Order,Guid>().Add(Order);

            // Save All Changes To the Database.
            await unitOfWork.SaveChangesAsync();

            // Convert Order to OrderToReturnDto to show it to the User.
            return mapper.Map<Order , OrderToReturnDto>(Order);
        }

        public Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethods()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string Email)
        {
            throw new NotImplementedException();
        }

        public Task<OrderToReturnDto> GetOrderByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
