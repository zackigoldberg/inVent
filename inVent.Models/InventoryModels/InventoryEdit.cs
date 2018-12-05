using inVent.Data;
using System.Collections.Generic;

namespace inVent.Web.Controllers
{
    public class InventoryEdit
    {
        public int InventoryId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public int ItemNumber { get; set; }
        public List<Item> Items { get; set; }

        public int FacilityId { get; set; }
        public List<Facility> Facilities { get; set; }


    }
}