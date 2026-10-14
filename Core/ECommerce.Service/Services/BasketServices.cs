using AutoMapper;
using ECommerce.Abstraction.IServices;
using ECommerce.Domain.Contratcs.Repos;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.Models.Baskets;
using ECommerce.Shared.Dtos.Basket_Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Services
{
    public class BasketServices : IBasketService
    {
        private readonly IBasketRepository basketRepository;
        private readonly IMapper mapper;

        public BasketServices(IBasketRepository _basketRepository , IMapper _mapper)
        {
            basketRepository = _basketRepository;
            mapper = _mapper;
        }
        public async Task<BasketDto> GetBasketAsync(string Key)
        {
            var Basket = await basketRepository.GetBasketAsync(Key);
            if (Basket is not null)
            {
                return mapper.Map<CustomerBasket, BasketDto>(Basket);
            }
            else
                throw new BasketNotFoundException(Key);
        }
        public async Task<BasketDto> CreateUpdateBasketAsync(BasketDto basket)
        {
            var CustomerBasket = mapper.Map<BasketDto, CustomerBasket>(basket);

            var SavedBasket = await basketRepository.CreateUpdateBasketAsync(CustomerBasket);

            if(SavedBasket is not null)
            {
                return await GetBasketAsync(SavedBasket.Id);
            }
            else
            {
                throw new Exception("Something Went Wrong During The Process.");
            }
        }

        public async Task<bool> DeleteBasketAsync(string Key)
        {
            return await basketRepository.DeleteBasketAsync(Key);
        }

      
    }
}
