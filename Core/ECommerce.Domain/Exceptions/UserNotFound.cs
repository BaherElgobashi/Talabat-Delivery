using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Exceptions
{
    public class UserNotFound(string Email) : NotFoundException($"User With Email {Email} is Not Found.")
    {
    }
}
