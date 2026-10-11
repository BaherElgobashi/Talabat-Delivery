using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.Common
{
    public class ProductQueryParams
    {
        private const int DefaultSize = 5;
        private const int MaximumSize = 10;
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public ProductSortingWay sortingWay { get; set; }
        public string? SearchValue { get; set; }

        public int PageIndex { get; set; } = 1;
        private int pageSize = DefaultSize;
        public int PageSize { get { return pageSize; } set {  pageSize = value > MaximumSize ? MaximumSize : value; } }
    }
}
