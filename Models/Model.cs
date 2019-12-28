using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace vega.Models
{
    [Table("Models")]
    public class Model
    {
        public int Id { get; set; }

        [StringLength(250, MinimumLength = 3)]  
        [Required]
        public string Name { get; set; }

        public Make Make { get; set; }
        public int MakeId { get; set; }
        public ICollection<ModelFeatures> ModelFeatures { get; set; }
        public Model()
        {
            ModelFeatures = new Collection<ModelFeatures>();
        }
    }
}
