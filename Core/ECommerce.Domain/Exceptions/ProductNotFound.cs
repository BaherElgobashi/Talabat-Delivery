using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Exceptions
{
    public class ProductNotFound(int Id) : NotFoundException($"Product With Id {Id} is not found")
    {
    }
}
