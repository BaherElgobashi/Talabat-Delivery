using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.DTOS.BasketDTOs
{
    public record BasketItemDTO(int Id , string ProductName , string PictureUrl , decimal Price , int Quantity);
    
}
