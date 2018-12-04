using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inVent.Data
{
    public enum FacilityType
    {
        [Display(Name = "Production")]
        Production = 0,
        [Display(Name = "Distribution")]
        Distribution,
        [Display(Name = "Retail")]
        Retail,
        [Display(Name = "Partner")]
        Partner
    }
    public class Facility
    {
        [Key]
        public int FacilityId { get; set; }
        [Required]
        public Guid OwnerId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public FacilityType Type { get; set; }
        [Required]
        public DateTimeOffset Opened { get; set;}
        public DateTimeOffset? Closed { get; set; }

        public Facility()
        {

        }
    }
}
