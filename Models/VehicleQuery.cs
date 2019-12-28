using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vega.Extensions;

namespace vega.Models
{
    public class VehicleQuery:IQueryObject
    {
        public int? MakeId { get; set; }
        public int? ModelId { get; set; }
        public String SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
        
    }
}
    