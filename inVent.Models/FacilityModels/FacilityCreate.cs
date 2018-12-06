using inVent.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inVent.Models.FacilityModels
{
    public  class FacilityCreate
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public FacilityType Type { get; set; }
    }
}

