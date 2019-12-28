using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using vega.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Model = vega.Models.Model;

namespace vega.Controllers.Resources
{
    public class VehicleResource
    {
        public int Id { get; set; }

        public bool IsRegistered { get; set; }

        public DateTime LastUpdate { get; set; }

        public ContactResource Contact { get; set; }

        public KeyValuePairResource Model { get; set; }

        public KeyValuePairResource Make { get; set; }

        public ICollection<KeyValuePairResource> VehicleFeatures { get; set; }

        public VehicleResource()
        {
            VehicleFeatures = new Collection<KeyValuePairResource>();
        }
    }
}
