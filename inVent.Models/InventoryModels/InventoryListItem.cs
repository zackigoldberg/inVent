using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inVent.Models.InventoryModels
{
    public class InventoryListItem
    {
        public int FacilityId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }
}
