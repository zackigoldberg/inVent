using inVent.Data;
using System.ComponentModel.DataAnnotations;

namespace inVent.Web.Controllers
{
    public class InventoryDetail
    {
        [Display(Name = "Facility Id")]
        public int FacilityId { get; set; }
        public Facility Facility { get; set; }
        [Display(Name = "Item Number")]
        public int ItemNumber { get; set; }
        public Item Item { get; set; }
        [Display(Name = "Inventory Id")]
        public int InventoryId { get; set; }
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}