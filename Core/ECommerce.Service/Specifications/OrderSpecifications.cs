using ECommerce.Domain.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Specifications
{
    public class OrderSpecifications : BaseSpecifications<Order, Guid>
    {
        public OrderSpecifications(string Email) : base( O => O.UserEmail == Email)
        {
            AddIncludes(O => O.DeliveryMethod);
            AddIncludes(O => O.Items);

            AddOrderByDesc(O => O.OrderDate);
        }
        public OrderSpecifications(Guid Id) : base(O => O.Id == Id)
        {
            AddIncludes(O => O.DeliveryMethod);
            AddIncludes(O => O.Items);
        }
    }
}
