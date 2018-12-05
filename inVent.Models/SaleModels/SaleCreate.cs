using inVent.Models.InventoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inVent.Models.SaleModels
{
    public class SaleCreate
    {
        public int InventoryId { get; set; }
        public List<InventoryListItem> Inventories { get; set; }
        public string Salesman { get; set; }
        public int QuantitySold { get; set; }
        public decimal SaleTotal { get; set; }
        public int FacilityId { get; set; }
        public int ItemNumber { get; set; }
    }
}
