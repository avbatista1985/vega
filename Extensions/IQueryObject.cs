using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vega.Extensions
{
    public interface IQueryObject
    {
         String SortBy { get; set; }
         bool IsSortAscending { get; set; }
         int Page { get; set; }
         byte PageSize { get; set; }
    }
}
