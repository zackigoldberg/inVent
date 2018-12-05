using inVent.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inVent.Models.SaleModels
{
    public class SaleEdit
    {
        public int SaleId { get; set; }
        public string Salesman { get; set; }
        public int FacilityId { get; set; }
        public List<Facility> Facilities { get; set; }
        public int ItemNumber { get; set; }
        public List<Item> Items { get; set; }
        public int InventoryId { get; set; }
        public int QuantitySold { get; set; }
        public decimal SaleTotal { get; set; }
    }
}
