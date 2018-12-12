using inVent.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace inVent.Web.Controllers
{
    public class InventoryEdit
    {
        [Display(Name = "Inventory Id")]
        public int InventoryId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        [Display(Name = "Item Number")]
        public int ItemNumber { get; set; }
        public List<Item> Items { get; set; }

        public int FacilityId { get; set; }
        public List<Facility> Facilities { get; set; }


    }
}