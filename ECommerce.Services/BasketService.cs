using AutoMapper;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Basket_Module;
using ECommerce.Services.Abstraction.Services;
using ECommerce.Shared.DTOS.BasketDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketService( IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        public async Task<BasketDTO> CreateOrUpdateBasketAsync(BasketDTO basket)
        {
            var CustomerBasket = _mapper.Map<BasketDTO , CustomerBasket>(basket);

            var CreatedOrUpdatedBasket = await _basketRepository.CreateOrUpdateAsync(CustomerBasket);

            return _mapper.Map<CustomerBasket , BasketDTO>(CreatedOrUpdatedBasket!);
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await _basketRepository.DeleteBasketAsync(id);
        }

        public Task<BasketDTO> GetBasketAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
