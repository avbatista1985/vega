using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vega.Models
{
    public class ModelFeatures
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public int FeatureId { get; set; }
        public Feature Feature { get; set; }

    }
}
