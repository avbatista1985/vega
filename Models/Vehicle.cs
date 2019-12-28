using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace vega.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string ContactName { get; set; }
        [Required]
        [StringLength(255)]
        public string ContactPhone { get; set; }

        [StringLength(255)]
        public string ContactEmail { get; set; }

        [Required]
        public int ModelId { get; set; }

        public Model Model { get; set; }

        [Required]
        public bool IsRegistered { get; set; }

        public DateTime LastUpdate { get; set; }

        public ICollection<VehicleFeatures> VehicleFeatures { get; set; }

        public ICollection<Photo> Photos { get; set; }

        public Vehicle()
        {
            VehicleFeatures = new Collection<VehicleFeatures>();
            Photos = new Collection<Photo>();
        }
    }
}
