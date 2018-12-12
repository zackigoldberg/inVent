using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using inVent.Data;

namespace inVent.Models.InventoryModels
{
    public class InventoryListItem
    {
        [Display(Name = "Inventory Id")]
        public int InventoryId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        [Display(Name = "Facility Id")]
        public int FacilityId { get; set; }
        public Facility Facility { get; set; }

        [Display(Name = "Item Number")]
        public int ItemNumber { get; set; }
        public Item Item { get; set; }
        
    }
}
