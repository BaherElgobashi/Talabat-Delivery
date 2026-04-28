using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities.Products
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
        public decimal Price { get; set; }

        #region Relationships.

        // Foregin Key.
        public int BrandId { get; set; }
        // Navigational Property.
        public ProductBrand ProductBrand { get; set; }


        // Foregin Key.
        public int TypeId { get; set; }

        // Navigational Property.
        public ProductType ProductType { get; set; }






        #endregion
    }
}
