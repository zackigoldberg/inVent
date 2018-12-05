using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using inVent.Data;
using inVent.Web.Models;

namespace inVent.Models.SaleModels
{
    public class SaleListItem
    {
        public int SaleId { get; set; }
        public string Salesman { get; set; }
        public int FacilityId { get; set; }
        public int ItemNumber { get; set; }
        public int InventoryId { get; set; }
        public int QuantitySold { get; set; }
        public decimal SaleTotal { get; set; }
        public Facility Facility { get; set; }
        public Item Item { get; set; }
        public Inventory Inventory { get; set; }
    }
}
