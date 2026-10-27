using AutoMapper;
using ECommerce.Abstraction.IServices;
using ECommerce.Domain.Contratcs.Repos;
using ECommerce.Domain.Contratcs.UOW;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.Models.Baskets;
using ECommerce.Domain.Models.Orders;
using ECommerce.Shared.Dtos.Basket_Dtos;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Services
{
    public class PaymentService(IConfiguration configuration , IBasketRepository basketRepository , IUnitOfWork unitOfWork , IMapper mapper) : IPaymentService
    {
        public async Task<BasketDto> CreateUpdatePaymentIntentAsync(string BasketId)
        {
            // 1. Configure Stripe : Install Stripe.Net.
            StripeConfiguration.ApiKey = configuration["StripeSettings:SecretKey"];

            // 2. Get Basket By BasketId.
            var Basket = await basketRepository.GetBasketAsync(BasketId) ?? throw new BasketNotFoundException(BasketId);

            // 3. Get Amount - GetProduct + DeliveryMethod Cost.
            var ProductRepo =  unitOfWork.GetRepository<Domain.Models.Products.Product, int>();

            foreach (var item in Basket.Items)
            {
                var Product = await ProductRepo.GetByIdAsync(item.Id) ?? throw new ProductNotFound(item.Id);
                item.Price = Product.Price;
            }

            var DeliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(Basket.DeliveryMethodId.Value)
                ?? throw new DeliveryMethodNotFoundException(Basket.DeliveryMethodId.Value);

            Basket.ShippingPrice = DeliveryMethod.Price;

            var BasketAmount = (long)((Basket.Items.Sum(item => item.Quantity * item.Price) + DeliveryMethod.Price) * 100);

            // 4. Create Payment Intent [Create - Update].
            var PaymentService = new PaymentIntentService();


            // Create Intent.
            if(Basket.PaymentIntentId is null)
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = BasketAmount,
                    Currency = "USD",
                    AllowedPaymentMethodTypes = ["card"]
                };

                var PaymentIntent = await PaymentService.CreateAsync(options);

                Basket.PaymentIntentId = PaymentIntent.Id;

                Basket.ClientSecret = PaymentIntent.ClientSecret;
            }

            // Update Intent.
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = BasketAmount,
                };

                await PaymentService.UpdateAsync(Basket.PaymentIntentId , options);
            }


            
            await basketRepository.CreateUpdateBasketAsync(Basket);

            return mapper.Map<CustomerBasket , BasketDto>(Basket);

        }
    }
}
