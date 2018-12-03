using inVent.Data;

namespace inVent.Web.Controllers
{
    public class InventoryEdit
    {
        public int InventoryId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public int ItemNumber { get; set; }
        public Item Item { get; set; }

        public int FacilityId { get; set; }
        public Facility Facility { get; set; }


    }
}