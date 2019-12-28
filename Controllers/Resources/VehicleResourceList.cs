using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace vega.Controllers.Resources
{
    public class VehicleResourceList
    {
        public int Id { get; set; }

        public bool IsRegistered { get; set; }

        public DateTime LastUpdate { get; set; }

        public ContactResource Contact { get; set; }

        public String ModelId { get; set; }

        public ICollection<KeyValuePairResource> VehicleFeatures { get; set; }

        public VehicleResourceList()
        {
            VehicleFeatures = new Collection<KeyValuePairResource>();
        }
    }
}
