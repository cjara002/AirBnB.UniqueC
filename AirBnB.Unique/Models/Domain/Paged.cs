using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirBnB.Unique.Models.Domain
{
    public class Paged<T> 
    {
        public int PageIndex { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPages { get; }
        public List<T> PagedItems { get; }

        public bool HasPreviousPage => PageIndex > 0;

        public bool HasNextPage => PageIndex + 1 < TotalPages;

  
        public Paged(List<T> data, int page, int pagesize, int totalCount)
        {
            PageIndex = page;
            PageSize = pagesize;
            PagedItems = data;

            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);

        }


    }
}
