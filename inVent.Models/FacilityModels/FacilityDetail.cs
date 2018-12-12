using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using inVent.Data;
using inVent.Models.InventoryModels;
using inVent.Models.SaleModels;
using inVent.Web.Models;

namespace inVent.Models.FacilityModels
{
    public class FacilityDetail
    {
        [Display(Name = "Facility Id")]
        public int FacilityId { get; set; }
        public string Name { get; set; }
        [Display(Name = "Facility Type")]
        public FacilityType Type { get; set; }
        public List<InventoryListItem> Inventories { get; set; }
        public List<SaleListItem> Sales { get; set; }
        public DateTimeOffset Opened { get; set; }
        public DateTimeOffset? Closed { get; set; }
    }
}
