using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.Common
{
    public class PaginationResult<TEntity>
    {
        public PaginationResult(int pageSize, int pageIndex, int totalCount, IEnumerable<TEntity> data)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            TotalCount = totalCount;
            Data = data;
        }

        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int TotalCount { get; set; }
        public  IEnumerable<TEntity> Data { get; set; }
    }
}
