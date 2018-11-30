using inVent.Data;

namespace inVent.Web.Models
{
    public class Inventory
    {
        [key]
        public int InventoryId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public int FacilityId { get; set; }
        public virtual Facility Facility { get; set; }

        public int ItemNumber { get; set; }
        public virtual Item Item  { get; set; }
    }
}