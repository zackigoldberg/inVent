using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using inVent.Data;

namespace inVent.Models.FacilityModels
{
    public class FacilityDetail
    {
        public int FacilityId { get; set; }
        public string Name { get; set; }
        public FacilityType Type { get; set; }
        public List<Item> Items { get; set; }
        public List<Sale> Sales { get; set; }
        public DateTimeOffset Opened { get; set; }
        public DateTimeOffset? Closed { get; set; }
    }
}
