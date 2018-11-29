using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using inVent.Data;

namespace inVent.Models.InventoryModels
{
    public class InventoryListItem
    {
        public int InventoryId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public int FacilityId { get; set; }
        public virtual Facility Facility { get; set; }

        public int ItemNumber { get; set; }
        public virtual Item Item { get; set; }
        
    }
}
