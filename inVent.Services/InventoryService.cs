using inVent.Data;
using inVent.Models.InventoryModels;
using inVent.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inVent.Services
{
    public class InventoryService
    {
        private readonly Guid _userId;

        public InventoryService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateInventory(InventoryCreate model)
        {
            var entity =
                new Inventory()
                {
                    FacilityId = model.FacilityId,
                    ItemId = model.ItemId,
                    Quantity = model.Quantity
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Inventories.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<InventoryListItem> GetAllInventory()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Inventories
                    .Select(
                        e =>
                        new InventoryListItem
                        {
                            FacilityId = e.FacilityId,
                            ItemId = e.ItemId,
                            Quantity = e.Quantity
                        }
                        );
                return query.ToArray();
            }
        }

        public IEnumerable<InventoryAdvancedListItem> GetFacilityInventory(int facilityId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                Dictionary<int, int> itemNumbers = new Dictionary<int, int>();
                List<InventoryAdvancedListItem> fakeList = new List<InventoryAdvancedListItem>();

                foreach (Inventory i in ctx.Inventories)
                {
                    if (i.FacilityId == facilityId)
                    {
                        itemNumbers.Add(i.ItemId, i.Quantity);
                    }
                }
                foreach (var kvp in itemNumbers)
                {
                    var query = ctx
                        .Items
                        .Where(e => e.ItemNumber == kvp.Key)
                        .Select(
                        e =>
                        new InventoryAdvancedListItem
                        {
                            FacilityId = facilityId,
                            ItemId = kvp.Key,
                            ItemName = e.Name,
                            Description = e.Description,
                            Quantity = kvp.Value
                        }
                        );
                    return query.ToArray();
                }
                return fakeList.ToArray();
            }
        }
    }
}
