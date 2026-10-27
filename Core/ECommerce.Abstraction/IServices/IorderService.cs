using ECommerce.Shared.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Abstraction.IServices
{
    public interface IOrderService
    {
        // Create Order.
        // Takes Two Parameters which are : 1- OrderDto[Address _ DeliveryMethod _ BasketId]. _ 2- UserEmail [By Using Token.].
        // It Returns : OrderToReturnDto[Id _ Email _ OrderDate _ Address _ OrderSate _ Items _ SubTotal _ Total].

        Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto , string Email);

        // Get All Delivery Methods.
        Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethods();

        // Get All Orders For Current Users.
        Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string Email);

        // Get Specific Order For Current User.
        Task<OrderToReturnDto> GetOrderByIdAsync(Guid Id);


    }
}
