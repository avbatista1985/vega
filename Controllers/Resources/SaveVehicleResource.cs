using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace vega.Controllers.Resources
{
    public class SaveVehicleResource
    {
        public int Id { get; set; }
        [Required]
        public int ModelId { get; set; }
        public KeyValuePairResource Model { get; set; }

        public KeyValuePairResource Make { get; set; }


        public bool IsRegistered { get; set; }

        public DateTime LastUpdate { get; set; }

        [Required]
        public ContactResource Contact { get; set; }

        public IList<int> VehicleFeatures { get; set; }

        public SaveVehicleResource()
        {
            VehicleFeatures = new Collection<int>();
        }
    }
}
