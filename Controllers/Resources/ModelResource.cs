using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vega.Models;

namespace vega.Controllers.Resources
{
    public class ModelResource: KeyValuePairResource
    {

        public IList<KeyValuePairResource> ModelFeatures { get; set; }

    }
}

