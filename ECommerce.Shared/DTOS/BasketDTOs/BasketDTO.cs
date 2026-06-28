using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.DTOS.BasketDTOs
{
    public class BasketDTO(string Id , ICollection<BasketItemDTO> Items);
    
}
