using ECommerce.Domain.Models.Products;
using ECommerce.Shared.Common;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Specifications
{
    public class ProductSpecifications : BaseSpecifications<Product, int>
    {
        // Get All Products Without Filtration.
        public ProductSpecifications(): base(null)
        {
            AddIncludes(p => p.Brand);
            AddIncludes(p => p.Type);
        }

        // Get All Products With BrandId and TypeId Filtration.
        public ProductSpecifications(ProductQueryParams productQueryParams) : 
            base(p => (!productQueryParams.BrandId.HasValue ||p.BrandId == productQueryParams.BrandId) && (!productQueryParams.TypeId.HasValue || p.TypeId == productQueryParams.TypeId)
            && (string.IsNullOrEmpty(productQueryParams.SearchValue) || p.Name.ToLower().Contains(productQueryParams.SearchValue.ToLower()) ))
        {
            AddIncludes(p => p.Brand);
            AddIncludes(p => p.Type);

            switch (productQueryParams.sortingWay) 
            {
                case ProductSortingWay.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;

                case ProductSortingWay.NameDesc:
                    AddOrderByDesc(p => p.Name);
                    break;

                case ProductSortingWay.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;

                case ProductSortingWay.PriceDesc:
                    AddOrderByDesc(p => p.Price);
                    break;

                default:
                    break;

                
            }

            ApplyPagination(productQueryParams.PageIndex, productQueryParams.PageSize);
        }

        // Get Product By Id and add Brand and Type To the Product.
        public ProductSpecifications(int id ):base(p => p.Id == id)
        {
            AddIncludes(p => p.Brand);
            AddIncludes(p => p.Type);
        }
    }
}
