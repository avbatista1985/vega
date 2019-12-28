using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace vega.Models
{
    public class Feature
    {
        public int Id { get; set; }
        [StringLength(50,MinimumLength =3)]
        [Required]
        public string Name { get; set; }
       
        public ICollection<VehicleFeatures> Modelssss { get; set; }
    }
}

