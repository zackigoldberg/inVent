using inVent.Data;

namespace inVent.Web.Controllers
{
    public class InventoryDetail
    {
        public int FacilityId { get; set; }
        public Facility Facility { get; set; }
        public int ItemNumber { get; set; }
        public Item Item { get; set; }
        public int InventoryId { get; set; }
        public int QuantitySold { get; set; }
        public decimal Price { get; set; }
    }
}