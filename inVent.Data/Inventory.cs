using inVent.Data;

namespace inVent.Web.Models
{
    public class Inventory
    {
        [key]
        public int InventoryId { get; set; }
        public int FacilityId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }
}